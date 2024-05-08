using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    public static SoundManager Instance { get; private set; }
    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }
    
 
    
    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.trash, (sender as TrashCounter).transform.position);
    }
    
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.objectDrop, (sender as BaseCounter).transform.position);
    }
    
    private void Player_OnPickedSomething(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }
    
    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        var cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }
    
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        var deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
        var deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume) {
        PlaySound(audioClipRefsSO.footsteps, position, volume);
    }
}