using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouthRotate : MonoBehaviour
{
    [SerializeField] private InputAction pressed, moved;

    private Camera cam;

    [SerializeField] private float rotationSpeed = 1f;
    private Vector2 rotation;

    private bool canRotate;
    private void Awake()
    {
        cam = Camera.main;

        pressed.Enable();
        moved.Enable();

        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { canRotate = false; };
        moved.performed += context => { rotation = context.ReadValue<Vector2>(); };
        moved.canceled += context => { rotation = Vector2.zero; };

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
