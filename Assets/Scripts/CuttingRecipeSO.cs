using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject {
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public int cuttingProgressMax;
}