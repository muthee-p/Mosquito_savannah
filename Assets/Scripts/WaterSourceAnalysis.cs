using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class WaterSourceAnalysis: MonoBehaviour {
    [SerializeField] private GameObject _watersorceAnalysisPanel;
    [SerializeField] private InventoryObject larvicideInventory;
    [SerializeField] private LarvicidePlantObject fuzzyPlant, plainPlant, snakePlant;
    [SerializeField] private TextMeshProUGUI _resultsAmountText, _sampleAmountText, _larvicideAmountText; 
    [SerializeField] private Image fuzzyPlantImage, plainPlantImage, snakePlantImage;
    private int _resultsAmount, _sampleAmount, _larvicideAmount; 
    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;


    void Start () {
        for (int i = 0; i< larvicideInventory.Container.Count; i++){
            if(larvicideInventory.Container[i].item ==fuzzyPlant){
                fuzzyPlant_amt =larvicideInventory.Container[i].amount;
                //fuzzyPlantImage.sprite = fuzzyPlant.sprite;
            }
            if(larvicideInventory.Container[i].item ==plainPlant){
                plainPlant_amt=larvicideInventory.Container[i].amount;
                //plainPlantImage.sprite = plainPlant.sprite;
            }
            if(larvicideInventory.Container[i].item == snakePlant){
                snakePlant_amt =larvicideInventory.Container[i].amount;
                //snakePlantImage.sprite = snakePlant.sprite;
            }
        }
        _sampleAmount= GameObject.FindGameObjectWithTag("water_source_1").GetComponent<TakeSample>()._sampleAmount;
        _sampleAmountText.text = "Sample = "+ _sampleAmount.ToString();
    }
    void Update(){
        CreateLarvicide();
        CreateResults();
    }
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
    void CreateResults(){
        if(_sampleAmount==1){
            _resultsAmount=1;
            _resultsAmountText.text="Results = 1";
            _sampleAmount =0;
            _sampleAmountText.text="Sample=0";
        }
    }
    void CreateLarvicide(){
        if(_resultsAmount == 1){
            if(fuzzyPlant_amt==1 && plainPlant_amt==2 && snakePlant_amt==3){
                _larvicideAmount=1;
                _larvicideAmountText.text="Larvicide " + _larvicideAmount.ToString();

                _resultsAmount=0;
                _resultsAmountText.text="Results = 0";
            }
        }
        
    }
    void UpdateImages(){

    }
}