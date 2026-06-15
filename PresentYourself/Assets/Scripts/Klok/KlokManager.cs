using UnityEngine;
using UnityEngine.InputSystem;

public class KlokManager : MonoBehaviour, IInteractable
{
    [Header("Clock Hands")] 
    [SerializeField] private Transform moonHand;
    [SerializeField] private Transform sunHand;

    [Header("Answer")] 
    [SerializeField] private int moonAnswer;
    [SerializeField] private int sunAnswer;

    private bool _controllingSun = true;
    private bool _isInRange = false;
    private bool _isInteracting = false;

    private int _moonPosition;
    private int _sunPosition;

    private InputAction _changeAction;
    private InputAction _switchAction;
    private InputAction _interactAction;

    private void Awake()
    {
        _changeAction = InputSystem.actions.FindAction("Change");
        _switchAction = InputSystem.actions.FindAction("Switch");
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnEnable()
    {
        _changeAction.Enable();
        _switchAction.Enable();
        _interactAction.Enable();
        
        _switchAction.performed += OnSwitch;
        _interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        _switchAction.performed -= OnSwitch;
        _interactAction.performed -= OnInteract;
        
        _changeAction.Disable();
        _switchAction.Disable();
        _interactAction.Disable();

        ExitInteraction();
    }

    public void SetRange(bool isInRange)
    {
        if (_isInteracting && !isInRange)
        {
            _isInteracting = false;
            ExitInteraction();
        }
        
        _isInRange = isInRange;
    }
    
    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!_isInRange)
            return;

        _isInteracting = !_isInteracting;

        if (_isInteracting)
            EnterInteraction();
        else
            ExitInteraction();
    }
    
    private void OnSwitch(InputAction.CallbackContext ctx)
    {
        if (!_isInteracting) return;

        _controllingSun = !_controllingSun;
    }
    
    public void EnterInteraction()
    {
        _changeAction.performed += OnChange;
        GameEvents.CanInteract?.Invoke(false);
        GameEvents.KlokInteraction?.Invoke(true);
    }
    
    public void ExitInteraction()
    {
        if (moonAnswer == _moonPosition && sunAnswer == _sunPosition)
        {
            Debug.Log("correct answer");
        }
            
        _changeAction.performed -= OnChange;

        if (_isInRange)
        {
            GameEvents.CanInteract?.Invoke(true);
        }
        else
        {
            GameEvents.CanInteract?.Invoke(false);
        }
        
        GameEvents.KlokInteraction?.Invoke(false);
    }

    private void OnChange(InputAction.CallbackContext ctx)
    {
        if (!_isInteracting) return;

        Vector2 input = ctx.ReadValue<Vector2>();

        if (input.y > 0.5f)
            Rotate(1);
        else if (input.y < -0.5f)
            Rotate(-1);
    }
    
    private void Rotate(int dir)
    {
        if (_controllingSun)
        {
            _sunPosition = (_sunPosition + dir + 12) % 12;
            sunHand.localRotation = Quaternion.Euler(0, 0, -_sunPosition * 30f);
        }
        else
        {
            _moonPosition = (_moonPosition + dir + 12) % 12;
            moonHand.localRotation = Quaternion.Euler(0, 0, -_moonPosition * 30f);
        }
    }

}
