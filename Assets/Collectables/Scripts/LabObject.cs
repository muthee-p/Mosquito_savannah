using UnityEngine;

[CreateAssetMenu(fileName = "New Lab Equipment", menuName = "Inventory/Collectables/LabEquipment")]
public class LabObject : ItemObject
{
    public float LabProcessingTime = 0.01f;
    public void Awake()
    {
        type = ItemType.Lab_equipment;
    }
}