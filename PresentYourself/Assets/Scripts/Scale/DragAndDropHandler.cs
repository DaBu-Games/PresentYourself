using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropHandler : MonoBehaviour
{
    [SerializeField] private ScaleManager scaleManager;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask draggableLayer;

    private ScaleWeight _heldObject;
    private float _distance;

    private InputAction _clickAction;
    private bool _isHolding;

    private void Awake()
    {
        _clickAction = InputSystem.actions.FindAction("Hold");
    }

    private void OnEnable()
    {
        _clickAction.Enable();

        _clickAction.started += OnPress;
        _clickAction.canceled += OnRelease;
    }

    private void OnDisable()
    {
        _clickAction.started -= OnPress;
        _clickAction.canceled -= OnRelease;

        _clickAction.Disable();
    }

    private void Update()
    {
        if (_isHolding && _heldObject)
            Drag();
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        TryPickup();

        if (_heldObject)
            _isHolding = true;
    }

    private void OnRelease(InputAction.CallbackContext ctx)
    {
        Drop();
        _isHolding = false;
    }

    private void TryPickup()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, draggableLayer))
        {
            if (hit.collider.TryGetComponent(out ScaleWeight weight))
            {
                _heldObject = weight;

                _distance = Vector3.Distance(
                    cam.transform.position,
                    weight.transform.position
                );
            }
        }
    }

    private void Drag()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        Vector3 point = ray.GetPoint(_distance);

        _heldObject.transform.position = point;
    }

    private void Drop()
    {
        if (_heldObject == null)
            return;

        if (!scaleManager.TryAddWeight(_heldObject))
        {
            scaleManager.RemoveWeight(_heldObject);
            _heldObject.ResetPosition();
        }

        _heldObject = null;
    }
}
