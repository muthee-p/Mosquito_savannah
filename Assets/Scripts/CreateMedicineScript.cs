using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateMedicineScript: MonoBehaviour {
  
   [SerializeField] private GameObject _createMedicinePanel, labProgressBarPanel,  medicinereadyPanel;
   [SerializeField] private InventoryObject _medicinalPlantsInventory;
   [SerializeField] private TextMeshProUGUI _lavenderText, _redheadText, _glowingText, _orangeflowerText, _medicineAmountText;
   [SerializeField] private  CurePlantObject lavenderPlant, redheadPlant, glowingPlant, orangeFlowerPlant;
   [SerializeField] private Button _createMedicineButton, _pickMedicineButton;
   [SerializeField] private TextMeshProUGUI _createMedicineButtonText, _pickMedicineButtonText;
   [SerializeField] private Image currentTask;
   [SerializeField] private Sprite medicineSprite;
   [SerializeField] private GameObject[] anylysisDevices;
    [SerializeField] private AnimationClip clip;
    private AudioSource successSound, collectSound;
    private int _lavenderAmount, _redheadAmount, _glowingAmount, _orangeflowerAmount;
    public int _medicineAmount, results;
    private float labProcessingTime;

    void Start(){
        successSound= GameObject.Find("successSound").GetComponent<AudioSource>();
         collectSound = GameObject.Find("collectSound").GetComponent<AudioSource>();
         labProcessingTime= clip.frameRate *400;
        //labProcessingTime = GameObject.Find("DropItems").GetComponent<LabCollectedItems>().labprocessingTime;
        for (int i = 0; i < anylysisDevices.Length; i++)
        {
             results = anylysisDevices[i].GetComponent<WaterSourceAnalysis>()._resultsAmount;
        }
       
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.M))
            {
                 CreateMedicine();
            }
         if (Input.GetKeyDown(KeyCode.P))
            {
                PickMedicine();
            }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _createMedicinePanel.SetActive(true);
            CheckPlantAmount();
            if(results<=1){
                ActivateCreateMedicineButton();
                _createMedicineButtonText.text="Create Medicine (M)";
            }
            else{
                _createMedicineButtonText.text="Results = 0";
                _pickMedicineButtonText.text = "No medicine";
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _createMedicinePanel.SetActive(false);
        }
    }
    
    void CheckPlantAmount(){
        for (int i = 0; i < _medicinalPlantsInventory.Container.Count; i++)
        {
            if(_medicinalPlantsInventory.Container[i].item == lavenderPlant){
                _lavenderAmount= _medicinalPlantsInventory.Container[i].amount;
                _lavenderText.text= _lavenderAmount.ToString() + " /3";
            }
            if(_medicinalPlantsInventory.Container[i].item == redheadPlant){
                _redheadAmount = _medicinalPlantsInventory.Container[i].amount;
                _redheadText.text = _redheadAmount.ToString()+" /1";
            }
            if(_medicinalPlantsInventory.Container[i].item == glowingPlant){
                _glowingAmount = _medicinalPlantsInventory.Container[i].amount;
                _glowingText.text= _glowingAmount.ToString()+" /2";
            }
            if(_medicinalPlantsInventory.Container[i].item == orangeFlowerPlant){
                _orangeflowerAmount= _medicinalPlantsInventory.Container[i].amount;
                _orangeflowerText.text= _orangeflowerAmount.ToString() + " /3";
            }
        } 
    }

    void ActivateCreateMedicineButton(){
        if(_lavenderAmount <=3 && _redheadAmount <=1 && _glowingAmount <=2 && _orangeflowerAmount<=3){
            
            _createMedicineButton.interactable = true;
        }
        
    }
    public void CreateMedicine(){
       collectSound.Play();
        labProgressBarPanel.SetActive(true);
        _pickMedicineButtonText.text = "Processing...";
        Invoke("MedicineReady", labProcessingTime);
    }

    public void MedicineReady(){ 
        _medicineAmount++;
        _medicineAmountText.text = "Medicine Amount = "+_medicineAmount;
        successSound.Play();
        medicinereadyPanel.SetActive(true);
        _pickMedicineButton.interactable=true;
        _pickMedicineButtonText.text = "Pick (P)";
        Invoke("HideMedicineReadyPane",1.5f);
    }
    public void PickMedicine(){
        collectSound.Play();
        _createMedicinePanel.SetActive(false);
        currentTask.sprite = medicineSprite;
    }
    private void HideMedicineReadyPane(){
         medicinereadyPanel.SetActive(false);
         labProgressBarPanel.SetActive(false);
    }
}