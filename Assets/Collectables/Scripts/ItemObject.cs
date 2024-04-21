using UnityEngine;

public enum ItemType{
    Lab_equipment,
    LarvicidePlant,
    CurePlant
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public Sprite sprite;
}
