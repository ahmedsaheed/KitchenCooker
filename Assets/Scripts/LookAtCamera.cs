using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    [SerializeField] private Mode mode = Mode.CameraForward;

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                var dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }

    private enum Mode {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }
}