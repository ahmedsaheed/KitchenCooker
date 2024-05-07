using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    [SerializeField] private RecipeListSO recipeListSO;
    private readonly float spawnRecipeTimerMax = 4f;
    private readonly int waitingRecipeMax = 4;
    private float spawnRecipeTimer;
    private List<RecipeSO> waitingRecipeSoList;
    public static DeliveryManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        waitingRecipeSoList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSoList.Count < waitingRecipeMax) {
                var waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log("Waiting Recipe: " + waitingRecipeSO.recipeName);
                waitingRecipeSoList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (var i = 0; i < waitingRecipeSoList.Count; i++) {
            var waitingRecipeSO = waitingRecipeSoList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                var plateContentMatchesRecipe = true;
                foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    var ingredientFound = false;
                    foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                        if (recipeKitchenObjectSO == plateKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }

                    if (!ingredientFound) plateContentMatchesRecipe = false;
                }

                if (plateContentMatchesRecipe) {
                    Debug.Log("Delivered Correct Recipe");
                    waitingRecipeSoList.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("Delivered Incorrect Recipe");
    }
}