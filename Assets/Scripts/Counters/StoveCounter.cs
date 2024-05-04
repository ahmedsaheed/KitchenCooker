using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private BurningRecipeSO burningRecipeSo;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float fryingTimer;
    private State state;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject())
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax) {
                        // Fried - destroy object and spawn fried version of the object
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSo.burningTimerMax
                    });
                    if (burningTimer >= burningRecipeSo.burningTimerMax) {
                        // Fried - destroy object and spawn fried version of the object
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }

                    break;
                case State.Burned:
                    break;
            }
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject())
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
        }
        else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
            }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectScriptableObject GetOutputForInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        var fryingRecipeSO = GetFryingRecipeSoWithInput(inputKitchenObjectScriptableObject);
        return fryingRecipeSO != null ? fryingRecipeSO.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        return GetFryingRecipeSoWithInput(inputKitchenObjectScriptableObject) != null;
    }

    private FryingRecipeSO GetFryingRecipeSoWithInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        foreach (var fryingRecipeSO in fryingRecipeSOArray)
            if (fryingRecipeSO.input == inputKitchenObjectScriptableObject)
                return fryingRecipeSO;
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSoWithInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        foreach (var burningRecipeSo in burningRecipeSOArray)
            if (burningRecipeSo.input == inputKitchenObjectScriptableObject)
                return burningRecipeSo;
        return null;
    }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }
}