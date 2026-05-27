using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(BoxCollider))]
public abstract class InteractableBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InteractableValues values;
    private HighlightState _state = HighlightState.None;
    private static readonly int HighlightStrength = Shader.PropertyToID("_HighlightStrength");
    public static readonly List<InteractableBase> All = new();
    
    protected bool hasInteracted = false;
    
    private SpriteRenderer _sr;
    private MaterialPropertyBlock _mpb;
    
    protected virtual void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
        All.Add(this);
    }

    protected virtual void Start()
    {
        if(_state == HighlightState.Selected)
            SetState(_state);
    }
    
    protected virtual void OnDestroy()
    {
        All.Remove(this);
    }
    
    protected virtual void OnExitSelected(Transform newTransform)
    {
        if(newTransform == transform)
            return;
        
        GameEvents.OnPointerClick -= OnExitSelected;
        SetState(HighlightState.Selectable);
    }
    
    public void HasInteracted() => hasInteracted = true;

    public void SetState(HighlightState state)
    {
        _state = state;
        
        SetHighlight(values.Get(state));
    }

    private void SetHighlight(float value)
    {
        _sr.GetPropertyBlock(_mpb);
        _mpb.SetFloat(HighlightStrength, value);
        _sr.SetPropertyBlock(_mpb);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SoulManager.IsBusy || SoulManager.InFocusMode)
            return;
        
        if(_state == HighlightState.Selectable)
            SetState(HighlightState.Highlighted);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_state != HighlightState.Highlighted)
            return;
        
        SetState(HighlightState.Selectable);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_state != HighlightState.Highlighted || SoulManager.IsBusy || SoulManager.InFocusMode)
            return;
        
        GameEvents.OnPointerClick?.Invoke(transform);
        GameEvents.OnPointerClick += OnExitSelected;
    }

    public abstract IEnumerator HandleInteraction();
}
