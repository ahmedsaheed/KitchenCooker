using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    private bool isWalking;
    private KitchenObject kitchenObject;
    private Vector3 lastInteractionDirection;
    private ClearCounter selectedCounter;

    public static Player Instance { get; private set; }

    private void Awake() {
        if (Instance != null) Debug.LogError("Multiple Player instance in the scene");

        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update() {
        MovementHandler();
        InteractionHandler();
    }

    public Transform GetKitchenObjectFollowTransform() {
        return KitchenObjectHoldPoint;
    }


    public void SetKitchenObject(KitchenObject Ko) {
        kitchenObject = Ko;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) selectedCounter.Interact(this);
    }

    private void InteractionHandler() {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) lastInteractionDirection = moveDir;

        var interactionDistance = 2f;
        // we are looking objects that has a physic collider in the direction of the last movement
        if (Physics.Raycast(transform.position, lastInteractionDirection, out var raycastHit,
                interactionDistance, counterLayerMask)) {
            // we found an object, is it a ClearCounter 
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // its a ClearCounter, proceed to interact   
                if (clearCounter != selectedCounter) SetSelectedCounter(clearCounter);
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }
    }

    private void MovementHandler() {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = .7f;
        var playerHeight = 2f;

        // check if theres a solid object in the way of the player
        var canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight, playerRadius,
            moveDir, moveDistance);

        if (!canMove) {
            // Try move on x & z axis
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight, playerRadius,
                moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            }
            else {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight, playerRadius,
                    moveDirZ, moveDistance);
                if (canMove) moveDir = moveDirZ;
                /* Can't move in any direction */
            }
        }

        if (canMove) transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;
        var rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }
}