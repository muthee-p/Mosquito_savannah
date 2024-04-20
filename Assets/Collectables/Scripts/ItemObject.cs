using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Lab_equipment,
    Plants,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
