using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndRotate : MonoBehaviour
{
    [SerializeField] private InputActionAsset mouseControls;
    private InputAction pressAction; 
    private InputAction moveAction;
    private InputAction positionAction;

    [SerializeField] private float rotationSpeed = 1f;
    private Vector2 rotation;

    private Vector3 movePos;
    private Vector3 offset;

    private Camera cam;

    private Transform clickedObject;
    private string clickedObjectTag;

    private bool canDrag;
    private bool canRotate;

    private void Awake()
    {
        cam = Camera.main;

        pressAction = mouseControls.FindActionMap("Mouse").FindAction("Press");
        moveAction = mouseControls.FindActionMap("Mouse").FindAction("Move");
        positionAction = mouseControls.FindActionMap("Mouse").FindAction("Position");

        positionAction.performed += context => movePos = context.ReadValue<Vector2>();

        moveAction.performed += context => rotation = context.ReadValue<Vector2>();
        moveAction.canceled += _ => rotation = Vector2.zero;

        pressAction.performed += _ => { if (clickedObjectTag == "Draggable") StartCoroutine(Drag()); };
        pressAction.performed += _ => { if (clickedObjectTag == "Rotatable") StartCoroutine(Rotate()); };
        pressAction.canceled += _ => { canDrag = false; canRotate = false; };
    }

    private void OnEnable()
    {
        positionAction.Enable();
        moveAction.Enable();
        pressAction.Enable();
    }

    private void OnDisable()
    {
        positionAction.Disable();
        moveAction.Disable();
        pressAction.Disable();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(movePos);
        RaycastHit hit;

        int jawLayerMask = LayerMask.GetMask("Jaw");
        int teethLayerMask = LayerMask.GetMask("Teeth");

        if (clickedObjectTag == "Draggable")
        {
            clickedObject.GetComponent<Outline>().enabled = false;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teethLayerMask))
        {
            clickedObject = hit.transform;
            clickedObjectTag = clickedObject.gameObject.tag;
            clickedObject.GetComponent<Outline>().enabled = true;
            canRotate = false;
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, jawLayerMask))
        {
            clickedObject = hit.transform;
            clickedObjectTag = clickedObject.gameObject.tag;
            canDrag = false;
        }
        else
        {
            clickedObject = null;
            clickedObjectTag = null;
            canDrag = false;
            canRotate = false;
        }
        Debug.Log(clickedObjectTag);
    }
    private IEnumerator Rotate()
    {
        canRotate = true;
        while (canRotate)
        {
            clickedObject.Rotate(Vector3.down, rotation.x * rotationSpeed * Time.deltaTime);
            clickedObject.Rotate(cam.transform.right, rotation.y * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private Vector3 MouseWorldPos()
    {
        movePos.z = cam.WorldToScreenPoint(clickedObject.position).z;
        return cam.ScreenToWorldPoint(movePos);
    }

    private IEnumerator Drag()
    {
        canDrag = true;
        offset = clickedObject.position - MouseWorldPos();

        while (canDrag)
        {
            clickedObject.position = MouseWorldPos() + offset;
            yield return null;
        }
    }
}
