using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounterVisual : MonoBehaviour {
    private Animator animator;
    private const string CUT = "Cut";

    [FormerlySerializedAs("containerCounter")] [SerializeField] private CuttingCounter cuttingCounter;
    
    private void Awake() {
        animator = GetComponent<Animator>();    
    }

    private void Start() {
        cuttingCounter.onCut += CuttingCounter_OnCut;    
        
    }
    
    private void CuttingCounter_OnCut(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
