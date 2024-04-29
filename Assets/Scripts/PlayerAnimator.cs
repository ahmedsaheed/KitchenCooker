using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private Player player;
    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    private void Awake() {
       animator = GetComponent<Animator>();
    }

    private void Update() {
       animator.SetBool(
              IS_WALKING, player.IsWalking() 
           );
    }
}
