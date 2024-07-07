using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float walkSpeed = 7.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float lookSpeed = 2;
    [SerializeField] private float lookXLimit = 45;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [SerializeField] private bool canMove = true;
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    [Header("Funtional Options")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool startingGame = true;
    [SerializeField] private bool zoomOver = false;


    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    private KeyCode interactKey = KeyCode.Mouse0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
      //  Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startingGame == true)
        {
            canMove = false;
        } else {

            if (zoomOver != true)
            {
                canMove = true;
                zoomOver = true;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float currentSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
            float currentSpeedZ = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;

            moveDirection.y = -0.1f;
            moveDirection = (forward * currentSpeedX) + (right * currentSpeedZ);

            characterController.Move(moveDirection * Time.deltaTime);

            if(canMove)
            {
                Cursor.lockState = CursorLockMode.Locked;

                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

                if(canInteract)
                {
                    HandleInteractionCheck();
                    HandleInteractionInput();
                }
            } else {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        } 
    }

    public void HandleInteractionCheck()
    {
        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            
            if(hit.collider.gameObject.layer == 6 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if(currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        } else if(currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    public void HandleInteractionInput()
    {
        if(Input.GetMouseButtonDown(0) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
        }
        if(Input.GetMouseButtonUp(0) && currentInteractable != null)
        {
            currentInteractable.OnStopInteract();
        }
    }
}
