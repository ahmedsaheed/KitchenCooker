using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    // Start is called before the first frame update
    private PlayerInputActions playerInputActions; 
    private void Awake() {
       playerInputActions =  new PlayerInputActions();
       playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 
        inputVector = inputVector.normalized;
        return inputVector;
    }
    
}