using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    // Start is called before the first frame update
    public static event EventHandler OnAnyObjectPlacedHere;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject Ko) {
        kitchenObject = Ko;
        if (Ko != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player) {
        // Debug.LogError("BaseCounter.Interact()");
    }
}