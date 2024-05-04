using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
    [SerializeField] private List<KitchenObjectScriptableObject> validKitchenObjectSOList;

    private List<KitchenObjectScriptableObject> kitchenObjectSOList;


    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectScriptableObject>();
    }

    public bool TryAddIngredient(KitchenObjectScriptableObject kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}