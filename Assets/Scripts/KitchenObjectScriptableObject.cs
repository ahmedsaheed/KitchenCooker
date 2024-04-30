using UnityEngine;

[CreateAssetMenu]
public class KitchenObjectScriptableObject : ScriptableObject {
    // Start is called before the first frame update
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}