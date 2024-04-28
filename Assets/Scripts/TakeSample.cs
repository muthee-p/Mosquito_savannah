using UnityEngine;
using UnityEngine.UI;

public class TakeSample : MonoBehaviour
{
    [SerializeField] private GameObject _takeSample, _disinfectPanel;
    [SerializeField] Image _currentTask;
    [SerializeField] private Sprite _fullTestTube;
    [SerializeField] GameObject analysisDevice;
    private Image currentTask;
    private Sprite larvicideSprite;
    public int _sampleAmount;
    void Start(){
        currentTask = analysisDevice.GetComponent<WaterSourceAnalysis>().currentTask;
        larvicideSprite = analysisDevice.GetComponent<WaterSourceAnalysis>().larvicideSprite;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(currentTask.sprite==larvicideSprite){
                _disinfectPanel.SetActive(true);
                Invoke("UpdateLarvicideApplication",1.5f);
            }else{
                _takeSample.SetActive(true);
                _currentTask.sprite = _fullTestTube;
            }
            
            Invoke("HidePanel", 1.5f);
        }
    }
    private void HidePanel(){
        if(_disinfectPanel.activeSelf){
            _disinfectPanel.SetActive(false);
        }
        else{
            _sampleAmount++;
            _takeSample.SetActive(false);
        }
        
        gameObject.GetComponent<BoxCollider>().enabled=false;
    }
     private void UpdateLarvicideApplication(){
        analysisDevice.GetComponent<WaterSourceAnalysis>().UpdateLarvicideApplication();
     }
}
