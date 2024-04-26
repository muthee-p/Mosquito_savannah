using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot2script : MonoBehaviour
{
   public Transform transformBot;

   void Update(){
        gameObject.GetComponent<NavMeshAgent>().SetDestination(transformBot.position);
   }
}
