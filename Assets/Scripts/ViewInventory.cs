using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventoryPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventoryPanel.SetActive(false);
        }
    }
}

