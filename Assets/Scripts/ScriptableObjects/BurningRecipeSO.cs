using UnityEngine;

[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject {
    public KitchenObjectScriptableObject input;
    public KitchenObjectScriptableObject output;
    public float burningTimerMax;
}