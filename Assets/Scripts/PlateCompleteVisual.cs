using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectScriptableObject kitchenObjectScriptableObject;
        public GameObject gameObject;
    }
    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var kitchenObjectSoGameObject in kitchenObjectSO_GameObjectList) {
               kitchenObjectSoGameObject.gameObject.SetActive(false); 
        }
    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (var kitchenObjectSoGameObject in kitchenObjectSO_GameObjectList) {
           if (kitchenObjectSoGameObject.kitchenObjectScriptableObject == e.kitchenObjectSO) 
               kitchenObjectSoGameObject.gameObject.SetActive(true); 
        }
    }

}