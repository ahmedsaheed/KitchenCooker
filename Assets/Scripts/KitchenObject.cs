using UnityEngine;

public class KitchenObject : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;
    private ClearCounter clearCounter;

    public KitchenObjectScriptableObject GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        if (this.clearCounter != null) this.clearCounter.ClearKitchenObject();

        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject()) {
            Debug.Log("Current counter has kitchen object!");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }
}