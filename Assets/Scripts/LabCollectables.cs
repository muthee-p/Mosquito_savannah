using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabCollactables : MonoBehaviour
{
    [SerializeField] private GameObject _labCollectablePanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _labCollectablePanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _labCollectablePanel.SetActive(false);
        }
    }
}

