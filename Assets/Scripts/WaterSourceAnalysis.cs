using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class WaterSourceAnalysis: MonoBehaviour {
    [SerializeField] private GameObject waterSourcePlane,_watersorceAnalysisPanel, ResultsPanel;
    [SerializeField] private InventoryObject larvicideInventory;
    [SerializeField] private LarvicidePlantObject fuzzyPlant, plainPlant, snakePlant;
    [SerializeField] public TextMeshProUGUI _resultsAmountText, _larvicideAmountText; 
    [SerializeField] public TextMeshProUGUI _sampleAmountText;
    [SerializeField] private Image fuzzyPlantImage, plainPlantImage, snakePlantImage;
    public Image currentTask;
     [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText;
     [SerializeField] private Button createLarvicideButton, analyseSampleButton;
     [SerializeField] public Button TakeSampleButton;
     public Sprite larvicideSprite;
    [SerializeField] Sprite _emptyTesttube, _emptySprite;
    [SerializeField]public TextMeshProUGUI disinfectedStatus;
     private AudioSource successSound, collectSound;
    private int _sampleAmount;
    public int  _larvicideAmount; 
    public int _resultsAmount;
    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;
    public int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;
    private string status;
    private bool canMakeLarvicide =false;
    public bool functionCalled=false;

    void Start () {
         plainPlantRecommendedAmount =  ResultsPanel.GetComponent<ResultsGenerator>().plainPlantRecommendedAmount;
        snakePlantRecommendedAmount = ResultsPanel.GetComponent<ResultsGenerator>().snakePlantRecommendedAmount;
        fuzzyPlantRecommendedAmount =  ResultsPanel.GetComponent<ResultsGenerator>().fuzzyPlantRecommendedAmount;
        successSound= GameObject.Find("successSound").GetComponent<AudioSource>();
        _sampleAmount= waterSourcePlane.GetComponent<TakeSampleScript>().GetSampleAmount();
        disinfectedStatus.text= "Untested";
        collectSound = GameObject.Find("collectSound").GetComponent<AudioSource>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.B))
        {
            AnalyseSample();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
           ClickCreateLarvicideButton();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(TakeSampleButton.interactable==true){
                TakeSample();
            }   
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            HideWaterSourceAnalysisPanel();
        }

        if(_resultsAmount<0){
            fuzzyPlantText.text="N/A";
            plainPlantText.text="N/A";
            snakePlantText.text="N/A";
        }
        if(disinfectedStatus.text=="Disinfected"){

            disinfectedStatus.color=Color.green;
        }
        if(functionCalled ==true){
             disinfectedStatus.text="Disinfected";
        disinfectedStatus.color=Color.green;
        }
        CheckPlantAmount();
        // _sampleAmount= waterSourcePlane.GetComponent<TakeSampleScript>()._sampleAmount;
        //_sampleAmountText.text = "Sample = "+ _sampleAmount;
         //_resultsAmountText.text="Results = "+_resultsAmount;
        //   _larvicideAmountText.text= "Larvicide = "+ _larvicideAmount;
         CreateResults();
    }
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             _sampleAmount= waterSourcePlane.GetComponent<TakeSampleScript>().GetSampleAmount();
              _sampleAmountText.text = "Sample = "+ _sampleAmount;
        
            _watersorceAnalysisPanel.SetActive(true);
            CheckPlantAmount();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(false);
        }
    }

    //WaterAnalysis
    public void TakeSample(){
      waterSourcePlane.GetComponent<BoxCollider>().enabled=true;
        currentTask.sprite =_emptyTesttube;
        _watersorceAnalysisPanel.SetActive(false);
        collectSound.Play();
    }
    public void HideWaterSourceAnalysisPanel(){
        _watersorceAnalysisPanel.SetActive(false);
    }

    void CreateResults(){
        if(_sampleAmount>=1){
             analyseSampleButton.interactable = true;
             disinfectedStatus.text="Untreated";
             disinfectedStatus.color = Color.yellow;
        }
    }
    public void AnalyseSample(){
        if(_sampleAmount>=1){
             ResultsPanel.GetComponent<Animator>().SetTrigger("processing");
        Invoke("AnalysisComplete", 4f);
        }
       
    }
    private void AnalysisComplete(){
        collectSound.Play();
        _resultsAmount++;
        _resultsAmountText.text="Results = "+_resultsAmount;
        _sampleAmount--;
        _sampleAmountText.text="Sample= "+ _sampleAmount--;
        CheckPlantAmount();
    }
     void CheckPlantAmount(){
        if(_resultsAmount >= 1){
            for (int i = 0; i< larvicideInventory.Container.Count; i++){
                if(larvicideInventory.Container[i].item ==fuzzyPlant){
                    fuzzyPlant_amt =larvicideInventory.Container[i].amount;
                    fuzzyPlantImage.sprite = larvicideInventory.Container[i].sprite;
                    fuzzyPlantText.text= fuzzyPlant_amt.ToString() + " /1" ;
                }
                if(larvicideInventory.Container[i].item ==plainPlant){
                    plainPlant_amt=larvicideInventory.Container[i].amount;
                    plainPlantImage.sprite = plainPlant.sprite;
                    plainPlantText.text = plainPlant_amt.ToString() + " /2" ;
                }
                if(larvicideInventory.Container[i].item == snakePlant){
                    snakePlant_amt =larvicideInventory.Container[i].amount;
                    snakePlantImage.sprite = snakePlant.sprite;
                    snakePlantText.text= snakePlant_amt.ToString() + " /3";
                }
            } 
            
            
            if (fuzzyPlant_amt>=1 && plainPlant_amt >= 2 && snakePlant_amt >= 3){
                createLarvicideButton.interactable = true;
                canMakeLarvicide=true;
            }
        }

       
    }
     public void ClickCreateLarvicideButton(){
        
        if(canMakeLarvicide==true){
            collectSound.Play();
            currentTask.sprite=larvicideSprite;
            _larvicideAmount++;
             _larvicideAmountText.text= "Larvicide = "+ _larvicideAmount++;
            _resultsAmount--; 
            ApplyLarvicide();
        }
       
    }
    
    
    private void ApplyLarvicide(){
        waterSourcePlane.GetComponent<BoxCollider>().enabled=true;
        for (int i = 0; i< larvicideInventory.Container.Count; i++){
                if(larvicideInventory.Container[i].item ==fuzzyPlant){
                    larvicideInventory.Container[i].amount-=1;
                }
                if(larvicideInventory.Container[i].item ==plainPlant){
                    larvicideInventory.Container[i].amount-=2;
                }
                if(larvicideInventory.Container[i].item == snakePlant){
                    larvicideInventory.Container[i].amount-=3;
                }
            }
    }

    public void UpdateLarvicideApplication(){
        currentTask.sprite = _emptySprite;
        successSound.Play();
        functionCalled=true;
    }
    
}