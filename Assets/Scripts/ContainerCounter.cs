using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    // Start is called before the first frame update
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;
    

    public override void Interact(Player player) {
        // spawn object and give it to player
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}