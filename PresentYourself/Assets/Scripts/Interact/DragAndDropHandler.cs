using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DragAndDropHandler : MonoBehaviour
{
    [SerializeField] private ScaleManager scaleManager;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private float maxInteractDistance = 3f;

    private ScaleWeight _heldObject;
    private float _distance;

    private InputAction _clickAction;
    private bool _isHolding;
    
    private IClickable _currentHover;

    private void Awake()
    {
        _clickAction = InputSystem.actions.FindAction("Hold");
    }

    private void OnEnable()
    {
        _clickAction.Enable();

        _clickAction.started += HandleClick;
        _clickAction.started += OnPress;
        _clickAction.canceled += OnRelease;
    }

    private void OnDisable()
    {
        _clickAction.started -= HandleClick;
        _clickAction.started -= OnPress;
        _clickAction.canceled -= OnRelease;

        _clickAction.Disable();
    }

    private void Update()
    {
        HandleHover();
        
        if (_isHolding && _heldObject)
            Drag();
    }
    
    private bool IsInRange(RaycastHit hit)
    {
        return Vector3.Distance(cam.transform.position, hit.collider.transform.position)
               <= maxInteractDistance;
    }

    private void HandleHover()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        IClickable hover = null;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, raycastLayer))
        {
            if (IsInRange(hit))
            {
                hit.collider.TryGetComponent(out hover);
            }
        }

        if (hover == _currentHover)
            return;

        _currentHover = hover;

        GameEvents.CanClick?.Invoke(_currentHover != null);
    }

    private void HandleClick(InputAction.CallbackContext ctx)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, raycastLayer))
        {
            if (!IsInRange(hit))
                return;
            
            if (hit.collider.TryGetComponent(out IClickable clickable))
            {
                clickable.OnClick();
            }
        }
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        if(scaleManager.IsCompleted)
            return;
        
        TryPickup();

        if (_heldObject)
            _isHolding = true;
    }

    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if(scaleManager.IsCompleted)
            return;
        
        Drop();
        _isHolding = false;
    }

    private void TryPickup()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, raycastLayer))
        {
            if (!IsInRange(hit))
                return;
            
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
