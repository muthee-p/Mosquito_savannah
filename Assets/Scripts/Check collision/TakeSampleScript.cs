using UnityEngine;
using UnityEngine.UI;

public class TakeSampleScript : MonoBehaviour
{
    [SerializeField] private GameObject waterAnalysisDevice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(waterAnalysisDevice.name == "waterAnalysisDevice")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysis>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice1")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisOne>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice2")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisTwo>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice3")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisThree>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice4")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFour>().WaterPanel();
            }
            if (waterAnalysisDevice.name == "waterAnalysisDevice5")
            {
                waterAnalysisDevice.GetComponent<WaterSourceAnalysisFive>().WaterPanel();
            }

        }
    }
}
