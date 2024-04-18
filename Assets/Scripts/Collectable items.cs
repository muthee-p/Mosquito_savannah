using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectableitems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}

