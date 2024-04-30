using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragOrRotate : MonoBehaviour
{
    [SerializeField] private InputActionAsset gameControls;
    private InputAction pressAction, dragAction, rotateAction;

    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float dragSpeed = 50f;

    private Vector2 rotateInput;
    private Vector2 rotation;

    private Vector3 movePos;
    private Vector3 offset;

    [SerializeField] private Transform[] snapPoints;
    [SerializeField] private float snapThreshold = 0.25f;

    private Camera cam;

    private Transform clickedObject;
    private string clickedObjectTag;

    private bool canDrag;
    private bool canRotate;

    private void Awake()
    {
        cam = Camera.main;

        pressAction = gameControls.FindActionMap("MouseControls").FindAction("Press");
        dragAction = gameControls.FindActionMap("MouseControls").FindAction("Drag");
        rotateAction = gameControls.FindActionMap("MouseControls").FindAction("Rotate");

        rotateAction.performed += context => rotateInput = context.ReadValue<Vector2>();
        rotateAction.canceled += _ => rotateInput = Vector2.zero;

        dragAction.performed += context => movePos = context.ReadValue<Vector2>();
        dragAction.canceled += _ => movePos = Vector2.zero;

        pressAction.performed += _ => StartCoroutine(Rotate(clickedObjectTag, clickedObject));
        pressAction.performed += _ => StartCoroutine(Drag(clickedObjectTag, clickedObject));
        pressAction.canceled += _ => PressEnd(clickedObjectTag, clickedObject);
    }

    private void _(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        pressAction.Enable();
        rotateAction.Enable();
        dragAction.Enable();
    }

    private void OnDisable()
    {
        pressAction.Disable();
        rotateAction.Disable();
        dragAction.Disable();
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
    }
    private IEnumerator Rotate(string tag, Transform transform)
    {
        if (tag == "Rotatable")
        {
            canRotate = true;
            while (canRotate)
            {
                rotation.y -= rotateInput.x * rotationSpeed;
                rotation.x += rotateInput.y * rotationSpeed;
                transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
                yield return null;
            }
        }
    }

    private Vector3 MouseWorldPos()
    {
        movePos.z = cam.WorldToScreenPoint(clickedObject.position).z;
        return cam.ScreenToWorldPoint(movePos);
    }

    private IEnumerator Drag(string tag, Transform transform)
    {
        if (tag == "Draggable")
        {
            canDrag = true;
            offset = transform.position - MouseWorldPos();

            while (canDrag)
            {
                Vector3 newPosition = MouseWorldPos() + offset;
                transform.position = Vector3.Lerp(transform.position, newPosition, dragSpeed);
                yield return null;
            }
        }
    }

    private void PressEnd(string tag, Transform transform)
    {
        canDrag = false;
        canRotate = false;

        if (tag == "Draggable")
        {
            float minDistance = float.MaxValue;
            Transform closestPoint = null;

            foreach (Transform point in snapPoints)
            {
                float distance = Vector3.Distance(transform.position, point.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = point;
                }
            }

            if (closestPoint != null && minDistance <= snapThreshold)
            {
                transform.position = closestPoint.position;
                transform.rotation = closestPoint.rotation;
            }           
        }
    }
}
