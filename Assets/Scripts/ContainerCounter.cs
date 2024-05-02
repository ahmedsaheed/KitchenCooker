using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;

    // Start is called before the first frame update
    public event EventHandler OnPlayerGrabbedObject;


    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // spawn object and give it to player, if the player doesn't have an object already
            var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}