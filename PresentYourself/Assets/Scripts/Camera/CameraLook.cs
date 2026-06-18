using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float sensitivity = 0.1f;

    private InputAction _lookAction;
    private float _xRotation;

    private void Awake()
    {
        _lookAction = InputSystem.actions.FindAction("Look");

        GameEvents.EndCredits += OnDisable;
        GameEvents.PauseUI += OnPause;
    }

    private void OnPause(bool paused)
    {
        if (paused) OnDisable(); else OnEnable();
    }

    private void OnEnable()
    {
        _lookAction?.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        _lookAction?.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        GameEvents.EndCredits -= OnDisable;
        GameEvents.PauseUI -= OnPause;
    }

    private void Update()
    {
        Vector2 lookInput = _lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
