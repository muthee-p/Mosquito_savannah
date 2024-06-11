using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HutScript : MonoBehaviour
{
    [SerializeField] private GameObject waterAnalysisDevice;
    [SerializeField] private GameObject i;
    public float lineOfSight, inRange;
   private Transform player;
   
   void Start(){
     player = GameObject.FindGameObjectWithTag("Player").transform;
   }

    void Update(){
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight){
            if (waterAnalysisDevice.name == "waterAnalysisDevice")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().enabled = true;

            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().enabled = true;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisTwo>().enabled = true;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice3")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisThree>().enabled = true;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice4")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFour>().enabled = true;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice5")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFive>().enabled = true;
            }

            if (distanceFromPlayer >= inRange)
            {
                i.SetActive(true);
            }
            
            else if (distanceFromPlayer <= inRange)
            {
                i.SetActive(false);
            }

        }
       
        else if (distanceFromPlayer > lineOfSight){
            if (waterAnalysisDevice.name == "waterAnalysisDevice")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisTwo>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice3")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisThree>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice4")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFour>().enabled = false;
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice5")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFive>().enabled = false;
            }
            i.SetActive (false);
        }
        
    }
     private  void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight) ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, inRange);
    }
}
