
using UnityEngine;

[CreateAssetMenu(fileName = "New Cure Plant Object", menuName = "Inventory/Collectables/CurePlant")]
public class CurePlantObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.CurePlant;
    }
}
