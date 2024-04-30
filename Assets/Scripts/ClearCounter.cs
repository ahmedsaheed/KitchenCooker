using UnityEngine;

public class ClearCounter : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectScriptableObject kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact() {
        Debug.Log("Interacting with ClearCounter");
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}