using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject {
    public List<KitchenObjectScriptableObject> kitchenObjectSOList;
    public string recipeName;
}