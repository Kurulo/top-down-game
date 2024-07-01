using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Main Info")]
    [SerializeField]
    protected new string name;

    [SerializeField]
    protected string description;

    [SerializeField]
    protected Sprite sprite;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public Sprite Sprite { get { return sprite; } }
}
