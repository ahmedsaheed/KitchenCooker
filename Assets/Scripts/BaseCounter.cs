using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    // Start is called before the first frame update
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject Ko) {
        kitchenObject = Ko;
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
}