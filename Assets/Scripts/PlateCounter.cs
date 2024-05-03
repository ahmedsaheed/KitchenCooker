using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {
    public event EventHandler OnPlateSpawned; 
    public event EventHandler OnPlateRemoved; 
    [SerializeField] private KitchenObjectScriptableObject plateKitchenObjectSO;
    
    private float spawnPlateTimer;
    private float spawnPlatewTimerMazx = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;
    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlatewTimerMazx) {
            spawnPlateTimer = 0f;
            if (plateSpawnedAmount < plateSpawnedAmountMax) {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (plateSpawnedAmount > 0) {
               plateSpawnedAmount--;
               KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
               OnPlateRemoved.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
