using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter)
            ShowVisual();
        else
            HideVisual();
    }

    private void ShowVisual() {
        foreach (var visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(true);
    }

    private void HideVisual() {
        foreach (var visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(false);
    }
}