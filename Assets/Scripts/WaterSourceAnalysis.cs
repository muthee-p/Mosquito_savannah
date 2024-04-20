using UnityEngine;

public class WaterSourceAnalysis: MonoBehaviour {
    [SerializeField] private GameObject _watersorceAnalysisPanel;

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(false);
        }
    }
}