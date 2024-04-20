using UnityEngine;

[CreateAssetMenu(fileName = "New larvicide Plant Object", menuName = "Inventory/Collectables/LarvicidePlant")]
public class LarvicidePlantObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.LarvicidePlant;
    }
}