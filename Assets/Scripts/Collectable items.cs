using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectableitems : MonoBehaviour
{
    public InventoryObject _inventory;
    private AudioSource collectSound;

    public ItemObject item;

    void Start(){
        collectSound = GameObject.Find("collectSound").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory.AddItem(item,1,item.sprite);
            collectSound.Play();
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}

