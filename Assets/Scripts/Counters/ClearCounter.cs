using UnityEngine;

public class ClearCounter : BaseCounter {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) player.GetKitchenObject().SetKitchenObjectParent(this);
            /* Player not carrying anything */
        }
        else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) {
                    // currently holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
                else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        // currently holding plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            player.GetKitchenObject().DestroySelf();
                }
            }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}