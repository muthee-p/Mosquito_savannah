using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
   [SerializeField] private GameObject playerAmature;
    private Animator animator;

    void Start(){
        animator = playerAmature.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterSource"))
        {
            animator.SetBool("hitWater", true);
        }
    }
     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WaterSource"))
        {
            animator.SetBool("hitWater", false);
        }
    }
}
