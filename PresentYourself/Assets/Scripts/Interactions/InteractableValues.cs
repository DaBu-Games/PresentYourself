using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InteractableValues", menuName = "ScriptableObjects/InteractableValues")]
public class InteractableValues : ScriptableObject
{
    [SerializeField] private List<HighlightEntry> highlightEntries;

    private Dictionary<HighlightState, float> _map;

    private void OnEnable()
    {
        BuildMap();
    }

    private void BuildMap()
    {
        _map = new Dictionary<HighlightState, float>();

        foreach (var entry in highlightEntries)
        {
            _map[entry.state] = entry.value;
        }
    }

    public float Get(HighlightState state)
    {
        if (_map == null)
            BuildMap();

        return _map.GetValueOrDefault(state, 0f);
    }
}
