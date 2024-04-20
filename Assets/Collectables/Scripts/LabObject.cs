using UnityEngine;

[CreateAssetMenu(fileName = "New Lab Equipment", menuName = "Inventory/Collectables/LabEquipment")]
public class LabObject : ItemObject
{
    public int LabProcessingTime;
    public void Awake()
    {
        type = ItemType.Lab_equipment;
    }
}