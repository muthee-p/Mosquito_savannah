using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutScript : MonoBehaviour
{
    [SerializeField] private GameObject waterAnalysisDevice;
    public float lineOfSight;
   private Transform player;
   
   void Start(){
     player = GameObject.FindGameObjectWithTag("Player").transform;
   }

    void Update(){
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight){
            if (waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().enabled = true;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().enabled = true;
            }
        }
       
        else if (distanceFromPlayer > lineOfSight){
            if (waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().enabled = false;
            }

        }
    }
     private  void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight) ;
    }
}
