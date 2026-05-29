using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InteractableValues", menuName = "ScriptableObjects/InteractableValues")]
public class InteractableValues : ScriptableObject
{
    [SerializeField] private List<HighlightEntry> highlightEntries;

    private Dictionary<HighlightState, Color> _map;

    private void OnEnable()
    {
        BuildMap();
    }

    private void BuildMap()
    {
        _map = new Dictionary<HighlightState, Color>();

        foreach (var entry in highlightEntries)
        {
            _map[entry.state] = entry.value;
        }
    }

    public Color Get(HighlightState state)
    {
        if (_map == null)
            BuildMap();

        return _map.GetValueOrDefault(state, new Color(1, 1, 1, 0));
    }
}
