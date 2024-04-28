using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot2script : MonoBehaviour
{
   [SerializeField] InventoryObject labinventory, medicinalPlantsInventory, larvicideInventory;
   [SerializeField] ItemObject[] itemObjects;
   [SerializeField] GameObject[] collectedObjects;
   public Transform transformBot;

   void Update(){
        gameObject.GetComponent<NavMeshAgent>().SetDestination(transformBot.position);
        ShowItems();
   }
   void ShowItems()
{
    // Iterate through each item object
    for (int i = 0; i < itemObjects.Length; i++)
    {
        bool itemFound = false; // Flag to track if the item is found in any inventory

        // Iterate through each inventory
        foreach (var inventory in new InventoryObject[] { labinventory, medicinalPlantsInventory, larvicideInventory })
        {
            // Check if the item is present in the current inventory
            for (int j = 0; j < inventory.Container.Count; j++)
            {
                if (inventory.Container[j].item == itemObjects[i] && inventory.Container[j].amount > 0)
                {
                    // If the item is found in any inventory, activate the corresponding collected object
                    collectedObjects[i].SetActive(true);
                    itemFound = true;
                    break; // Exit the loop since the item is found in this inventory
                }
            }

            if (itemFound)
            {
                break; // Exit the loop since the item is found in any inventory
            }
        }

        // If the item is not found in any inventory, deactivate the corresponding collected object
        if (!itemFound)
        {
            collectedObjects[i].SetActive(false);
        }
    }
}

}
