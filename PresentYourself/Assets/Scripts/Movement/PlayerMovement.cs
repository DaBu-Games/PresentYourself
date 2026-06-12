using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private Transform cameraTransform;

    private InputAction _moveAction;
    private InputAction _sprintAction;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    private void OnEnable()
    {
        _moveAction?.Enable();
        _sprintAction?.Enable();
    }

    private void OnDisable()
    {
        _moveAction?.Disable();
        _sprintAction?.Disable();
    }

    private void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        
        if(input == Vector2.zero)
            return;
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * input.y + right * input.x;
        
        float speed = _sprintAction.IsPressed() ? sprintSpeed : movementSpeed;
        
        transform.position += moveDir * (speed * Time.deltaTime);
    }
}
