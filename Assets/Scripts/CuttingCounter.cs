using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject())
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    player.GetKitchenObject().SetKitchenObjectParent(this);
        }
        else {
            if (player.HasKitchenObject()) { }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // destroy object and spawn cut version of the object
            var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectScriptableObject GetOutputForInput(
        KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        foreach (var cuttingRecipeSo in cuttingRecipeSoArray)
            if (cuttingRecipeSo.input == inputKitchenObjectScriptableObject)
                return cuttingRecipeSo.output;

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectScriptableObject) {
        foreach (var cuttingRecipeSo in cuttingRecipeSoArray)
            if (cuttingRecipeSo.input == inputKitchenObjectScriptableObject)
                return true;
        return false;
    }
}