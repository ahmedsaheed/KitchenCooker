using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
   
   [SerializeField] private KitchenObjectScriptableObject cutKitchenObjectSO;
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
   
   public override void InteractAlternate(Player player) {
      if (HasKitchenObject()) {
         // destroy object and spawn cut version of the object
         GetKitchenObject().DestroySelf();
         KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
      }
   }
}
