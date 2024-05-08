using System;
using UnityEngine;
using UnityEngine.Video;

public class CuttingCounter : BaseCounter, IHasProgress {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProgress;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler onCut;
    public static event EventHandler OnAnyCut;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject())
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    var cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                    });
                }
        }
        else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
            }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // destroy object and spawn cut version of the object
            cuttingProgress++;
            onCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            var cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = cuttingProgress / (float)cuttingRecipeSo.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSo.cuttingProgressMax) {
                var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectScriptableObject GetOutputForInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        var cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectScriptableObject);
        return cuttingRecipeSo != null ? cuttingRecipeSo.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        return GetCuttingRecipeSoWithInput(inputKitchenObjectScriptableObject) != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSoWithInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        foreach (var cuttingRecipeSo in cuttingRecipeSoArray)
            if (cuttingRecipeSo.input == inputKitchenObjectScriptableObject)
                return cuttingRecipeSo;
        return null;
    }
}