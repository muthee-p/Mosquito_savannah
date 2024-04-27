using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Collectableitems : MonoBehaviour
{
    public InventoryObject _inventory;
    private AudioSource collectSound;

    public ItemObject item;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float ShakeIntensity =1f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmp;

    void Awake(){
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start(){
        collectSound = GameObject.Find("collectSound").GetComponent<AudioSource>();
        GameObject.Find("VCamera").GetComponent<CameraShake>().StopShake();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory.AddItem(item,1,item.sprite);
            collectSound.Play();
            Destroy(gameObject);
             GameObject.Find("VCamera").GetComponent<CameraShake>().ShakeCamera();
            if(timer >0){
                timer -=Time.deltaTime;
                if(timer <=0){
                    GameObject.Find("VCamera").GetComponent<CameraShake>().StopShake();
                }
            }
        }
    }
}

