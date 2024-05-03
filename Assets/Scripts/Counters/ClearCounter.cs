using UnityEngine;

public class ClearCounter : BaseCounter {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else {
            if (player.HasKitchenObject()) { }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}