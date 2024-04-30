using UnityEngine;

public class KitchenObject : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;

    public KitchenObjectScriptableObject GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}