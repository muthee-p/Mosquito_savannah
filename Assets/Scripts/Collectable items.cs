using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectableitems : MonoBehaviour
{
    public InventoryObject labInventory;

    public ItemObject item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            labInventory.AddItem(item,1,item.sprite);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}

