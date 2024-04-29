using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float upDownRange = 20f;

    private float rotationVertical = 0f;
    private float rotationHorizontal = 0f;

    private Camera mainCamera;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        Vector3 inputMove = new Vector3 (horizontalInput, 0, verticalInput);
        inputMove = transform.rotation * inputMove;

        characterController.Move(inputMove);
    }
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;   
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationHorizontal += mouseX;
        rotationVertical -= mouseY;
        rotationVertical = Mathf.Clamp(rotationVertical, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(rotationVertical, rotationHorizontal, 0);
    }
}
