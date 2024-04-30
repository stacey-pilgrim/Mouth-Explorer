using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouthRotate : MonoBehaviour
{
    [SerializeField] private InputActionAsset mouseControls;
    private InputAction pressAction;
    private InputAction moveAction;

    private Camera cam;

    [SerializeField] private float rotationSpeed = 1f;
    private Vector2 rotation;

    public bool canRotate;
    private void Awake()
    {
        cam = Camera.main;

        pressAction = mouseControls.FindActionMap("Mouse").FindAction("Press");
        moveAction = mouseControls.FindActionMap("Mouse").FindAction("Move");

        pressAction.performed += _ => { StartCoroutine(Rotate()); };
        pressAction.canceled += _ => { canRotate = false; };
        moveAction.performed += context => { rotation = context.ReadValue<Vector2>(); };
        moveAction.canceled += context => { rotation = Vector2.zero; };

    }

    private void OnEnable()
    {
        pressAction.Enable();
        moveAction.Enable();
    }

    private void OnDisable()
    {
        pressAction.Disable();
        moveAction.Disable();
    }

    private IEnumerator Rotate()
    {
        canRotate = true;
        while(canRotate)
        {
            transform.Rotate(Vector3.down, rotation.x * rotationSpeed, Space.World);
            transform.Rotate(cam.transform.right, rotation.y * rotationSpeed, Space.World);
            yield return null;
        }
    }
}
