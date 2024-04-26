using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WaterSourceAnalysis: MonoBehaviour {
    [SerializeField] private GameObject _watersorceAnalysisPanel;
    [SerializeField] private InventoryObject larvicideInventory;
    [SerializeField] private LarvicidePlantObject fuzzyPlant, plainPlant, snakePlant;
    [SerializeField] private TextMeshProUGUI _resultsAmountText, _sampleAmountText, _larvicideAmountText; 
    [SerializeField] private Image fuzzyPlantImage, plainPlantImage, snakePlantImage, currentTask;
     [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText;
     [SerializeField] private Button createLarvicideButton, analyseSampleButton;
     [SerializeField] private Sprite larvicideSprite;
    private int _resultsAmount, _sampleAmount, _larvicideAmount; 
    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;


    void Start () {
        _sampleAmount= GameObject.FindGameObjectWithTag("water_source_1").GetComponent<TakeSample>()._sampleAmount;
        _sampleAmountText.text = "Sample = "+ _sampleAmount;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
    }
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(true);
            CheckPlantAmount();
            CreateResults();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(false);
        }
    }

    void CreateResults(){
        if(_sampleAmount<=1){
             analyseSampleButton.interactable = true;
        }
    }
    public void AnalyseSample(){
        _resultsAmount++;
        _resultsAmountText.text="Results = "+_resultsAmount;
        _sampleAmount --;
        _sampleAmountText.text="Sample= "+ _sampleAmount;
    }
     void CheckPlantAmount(){
        if(_resultsAmount <= 1){
            for (int i = 0; i< larvicideInventory.Container.Count; i++){
                if(larvicideInventory.Container[i].item ==fuzzyPlant){
                    fuzzyPlant_amt =larvicideInventory.Container[i].amount;
                    fuzzyPlantImage.sprite = larvicideInventory.Container[i].sprite;
                    fuzzyPlantText.text= fuzzyPlant_amt.ToString() + " /4";
                }
                if(larvicideInventory.Container[i].item ==plainPlant){
                    plainPlant_amt=larvicideInventory.Container[i].amount;
                    plainPlantImage.sprite = plainPlant.sprite;
                    plainPlantText.text = plainPlant_amt.ToString() + " /3";
                }
                if(larvicideInventory.Container[i].item == snakePlant){
                    snakePlant_amt =larvicideInventory.Container[i].amount;
                    snakePlantImage.sprite = snakePlant.sprite;
                    snakePlantText.text= snakePlant_amt.ToString() + " /5";
                }
            }
        }

        if (fuzzyPlant_amt<=4 && plainPlant_amt <= 3 && snakePlant_amt <= 5){
            createLarvicideButton.interactable = true;
            _larvicideAmount++;
                _larvicideAmountText.text="Larvicide " + _larvicideAmount;

                _resultsAmount--;
                _resultsAmountText.text="Results = " + _resultsAmount;
        }
    }
     public void ClickCreateLarvicideButton(){
        _larvicideAmount++;
        _larvicideAmountText.text= "Larvicide = "+ _larvicideAmount;
        currentTask.sprite=larvicideSprite;
    }
    
}