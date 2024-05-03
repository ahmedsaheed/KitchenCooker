using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public float fryingTimerMax;
}