using UnityEngine;
using UnityEngine.UI;

public class TakeSampleScript : MonoBehaviour
{
    [SerializeField] private GameObject waterAnalysisDevice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().WaterPanel();
            }

        }
    }
}
