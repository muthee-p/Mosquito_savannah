using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class WaterSourceAnalysis: MonoBehaviour {
    [SerializeField] private GameObject waterSourcePlane,_watersorceAnalysisPanel, ResultsPanel;
    [SerializeField] private InventoryObject larvicideInventory;
    [SerializeField] private LarvicidePlantObject fuzzyPlant, plainPlant, snakePlant;
    [SerializeField] private TextMeshProUGUI _resultsAmountText, _sampleAmountText, _larvicideAmountText; 
    [SerializeField] private Image fuzzyPlantImage, plainPlantImage, snakePlantImage;
    public Image currentTask;
     [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText;
     [SerializeField] private Button createLarvicideButton, analyseSampleButton;
     public Sprite larvicideSprite;
    [SerializeField] Sprite _emptyTesttube, _emptySprite;
    [SerializeField]private TextMeshProUGUI disinfectedStatus;
     private AudioSource successSound;
    private int _sampleAmount;
    public int  _larvicideAmount; 
    public int _resultsAmount;
    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;
    private int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;

    void Start () {
        successSound= GameObject.Find("successSound").GetComponent<AudioSource>();
        _sampleAmount= waterSourcePlane.GetComponent<TakeSample>()._sampleAmount;
        disinfectedStatus.text= "Untested";
        plainPlantRecommendedAmount = ResultsPanel.GetComponent<ResultsGenerator>().plainPlantRecommendedAmount;
        snakePlantRecommendedAmount = ResultsPanel.GetComponent<ResultsGenerator>().snakePlantRecommendedAmount;
        fuzzyPlantRecommendedAmount = ResultsPanel.GetComponent<ResultsGenerator>().fuzzyPlantRecommendedAmount;

    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.A))
        {
            AnalyseSample();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ClickCreateLarvicideButton();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeSample();
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
        
        _sampleAmountText.text = "Sample = "+ _sampleAmount;
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

    //WaterAnalysis
    public void TakeSample(){
      waterSourcePlane.GetComponent<BoxCollider>().enabled=true;
        currentTask.sprite =_emptyTesttube;
        _watersorceAnalysisPanel.SetActive(false);
    }
    public void HideWaterSourceAnalysisPanel(){
        _watersorceAnalysisPanel.SetActive(false);
    }

    void CreateResults(){
        if(_sampleAmount<=1){
             analyseSampleButton.interactable = true;
             disinfectedStatus.text="Untreated";
             disinfectedStatus.color = Color.yellow;
        }
    }
    public void AnalyseSample(){
        ResultsPanel.GetComponent<Animator>().SetTrigger("processing");
        Invoke("AnalysisComplete", 4f);
    }
    private void AnalysisComplete(){
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
                    fuzzyPlantText.text= fuzzyPlant_amt.ToString() + " /" + fuzzyPlantRecommendedAmount;
                }
                if(larvicideInventory.Container[i].item ==plainPlant){
                    plainPlant_amt=larvicideInventory.Container[i].amount;
                    plainPlantImage.sprite = plainPlant.sprite;
                    plainPlantText.text = plainPlant_amt.ToString() + " /" + plainPlantRecommendedAmount;
                }
                if(larvicideInventory.Container[i].item == snakePlant){
                    snakePlant_amt =larvicideInventory.Container[i].amount;
                    snakePlantImage.sprite = snakePlant.sprite;
                    snakePlantText.text= snakePlant_amt.ToString() + " /" + snakePlantRecommendedAmount;
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
        ApplyLarvicide();
    }
    private void ApplyLarvicide(){
        waterSourcePlane.GetComponent<BoxCollider>().enabled=true;
    }

    public void UpdateLarvicideApplication(){
        _larvicideAmount--;
        currentTask.sprite = _emptySprite;
        successSound.Play();
        disinfectedStatus.text="Disinfected";
        disinfectedStatus.color=Color.green;
    }
    
}