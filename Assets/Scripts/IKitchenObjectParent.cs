using UnityEngine;

public interface IKitchenObjectParent {
    // Start is called before the first frame update
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObject Ko);
    public KitchenObject GetKitchenObject();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}