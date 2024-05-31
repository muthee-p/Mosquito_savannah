using UnityEngine;
using UnityEngine.UI;

public class TakeSampleScript : MonoBehaviour
{
    [SerializeField] private GameObject _takeSample, _disinfectPanel;
    [SerializeField] Image _currentTask;
    [SerializeField] private Sprite _fullTestTube;
    [SerializeField] GameObject analysisDevice;
    private Image currentTask;
    private Sprite larvicideSprite;
    private Button TakeSampleButton;
    public int _sampleAmount, _larvicideAmount;
    private string  _sampleAmountText, _larvicideAmountText;

    void Start(){
        currentTask = analysisDevice.GetComponent<WaterSourceAnalysis>().currentTask;
        larvicideSprite = analysisDevice.GetComponent<WaterSourceAnalysis>().larvicideSprite;
        _larvicideAmount = analysisDevice.GetComponent<WaterSourceAnalysis>()._larvicideAmount;
        _sampleAmountText= analysisDevice.GetComponent<WaterSourceAnalysis>()._sampleAmountText.text;
        _larvicideAmountText= analysisDevice.GetComponent<WaterSourceAnalysis>()._larvicideAmountText.text;
        TakeSampleButton= analysisDevice.GetComponent<WaterSourceAnalysis>().TakeSampleButton;
        
    }
    void Update(){
        _sampleAmountText = "Sample =" + _sampleAmount;
        _larvicideAmountText= "Larvicide = " + _larvicideAmount;
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
                _sampleAmount++;
               
                TakeSampleButton.interactable=false;
            }
            
            Invoke("HidePanel", 1.5f);
        }
    }
    private void HidePanel(){
        if(_disinfectPanel.activeSelf){
            _disinfectPanel.SetActive(false);
            _larvicideAmount--;
            TakeSampleButton.interactable=true;
        }
        else{
            
            _takeSample.SetActive(false);
        }
        
        gameObject.GetComponent<BoxCollider>().enabled=false;
    }
     private void UpdateLarvicideApplication(){
        analysisDevice.GetComponent<WaterSourceAnalysis>().UpdateLarvicideApplication();
        analysisDevice.GetComponent<WaterSourceAnalysis>().functionCalled=true;
     }
      public int GetSampleAmount()
    {
        return _sampleAmount;
    }
}
