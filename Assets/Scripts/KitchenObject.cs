using UnityEngine;

public class KitchenObject : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectScriptableObject GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) this.kitchenObjectParent.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject()) Debug.Log("Current kitchen object parent has kitchen object!");
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
}