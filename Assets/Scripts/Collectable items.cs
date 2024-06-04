using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Collectableitems : MonoBehaviour
{
    [SerializeField] private GameObject scriptHolder;
    private CollectablesSpawner collectablesSpawner;
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
        collectablesSpawner = scriptHolder.GetComponent<CollectablesSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory.AddItem(item,1,item.sprite);
            collectablesSpawner.RespawnPrefab(gameObject);
            collectSound.Play();
            gameObject.SetActive(false);
            Invoke("DestroyPrefab", 3);
             GameObject.Find("VCamera").GetComponent<CameraShake>().ShakeCamera();
            Invoke("StopShake", 1);
        }
    }
    private void StopShake(){
        GameObject.Find("VCamera").GetComponent<CameraShake>().StopShake();

    }
    private void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}

