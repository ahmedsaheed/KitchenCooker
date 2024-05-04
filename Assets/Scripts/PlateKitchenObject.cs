using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
    [SerializeField] private List<KitchenObjectScriptableObject> validKitchenObjectSOList;

    private List<KitchenObjectScriptableObject> kitchenObjectSOList;


    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectScriptableObject>();
    }

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public bool TryAddIngredient(KitchenObjectScriptableObject kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) return false;
        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            kitchenObjectSO = kitchenObjectSO
        });
        return true;
    }

    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectScriptableObject kitchenObjectSO;
    }
}