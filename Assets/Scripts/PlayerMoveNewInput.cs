using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveNewInput : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float mouseSensitivity = 0.5f;
    [SerializeField] float upDownRange = 20f;

    private float rotationVertical = 0f;

    private Camera mainCamera;
    private CharacterController characterController;

    [SerializeField] private InputActionAsset playerControls;
    private InputAction activateAction;
    private InputAction moveAction;
    private InputAction lookAction;
    private Vector2 moveInput;
    private Vector2 lookInput;
    void Awake()
    {   
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        activateAction = playerControls.FindActionMap("Player").FindAction("Activate");
        moveAction = playerControls.FindActionMap("Player").FindAction("Move");
        lookAction = playerControls.FindActionMap("Player").FindAction("Look");

        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;
        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

    }

    private void OnEnable()
    {
        activateAction.Enable();
        moveAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable()
    {
        activateAction.Disable();
        moveAction.Disable();
        lookAction.Disable();
    }

    void Update()
    {
        if (activateAction.ReadValue<float>() == 0)
            return;

        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float verticalInput = moveInput.y * moveSpeed;
        float horizontalInput = moveInput.x * moveSpeed;

        Vector3 inputMove = new Vector3(horizontalInput, 0, verticalInput);
        inputMove = transform.rotation * inputMove;

        characterController.Move(inputMove * Time.deltaTime);
    }
    void HandleRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        float mouseY = lookInput.y * mouseSensitivity;
        rotationVertical -= mouseY;
        rotationVertical = Mathf.Clamp(rotationVertical, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(rotationVertical, 0, 0);
    }
}