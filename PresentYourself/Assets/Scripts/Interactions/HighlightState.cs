public enum HighlightState
{
    None,
    Selectable,
    Highlighted,
    Selected,
}

[System.Serializable]
public struct HighlightEntry
{
    public HighlightState state;
    public float value;
}
