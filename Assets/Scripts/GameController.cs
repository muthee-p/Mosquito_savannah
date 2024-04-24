using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _waterSourceAnalysisPanel, _resultsPanel;
    [SerializeField] private InventoryObject _labInventory, _medicinalPlantsInventory, _larvicidePlantInventory;
    [SerializeField] private Image[] _itemImages;
    [SerializeField] private TextMeshProUGUI[] _itemAmountText;
    [SerializeField] private TextMeshProUGUI _heading;
    [SerializeField] Button labButton, MedicineButton, larvicideButton;
    [SerializeField] Image _currentTask;
    [SerializeField] Sprite _emptyTesttube;
    private InventoryObject _currentInventory;

    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
        _currentInventory=_labInventory;
    }

    void Update(){
        InventoryButtonsController();
    }
    public void OpenInventoryPanel(){
        _inventoryPanel.SetActive(true);
        _currentInventory=_labInventory;
    }

    public void DisplayLabInventory()
    {
        _heading.text = "Lab Inventory";
        ResetValues();
        _currentInventory=_labInventory;
    }

    public void DisplayMedicinalPlantsInventory()
    {
        _heading.text = "Medicinal Plants Inventory";
        ResetValues();
        _currentInventory=_medicinalPlantsInventory;
    }

    public void DisplayLarvicidePlantInventory()
    {
        _heading.text = "Larvicide Plants Inventory";
        ResetValues();
        _currentInventory=_larvicidePlantInventory;
        
    }

    private void InventoryDisplayController(InventoryObject _inventory){
        for (int i = 0; i < _inventory.Container.Count; i++)
        {
            _itemAmountText[i].text = _inventory.Container[i].amount.ToString();
            _itemImages[i].sprite = _inventory.Container[i].sprite;
        }  
    }

    private void InventoryButtonsController(){
        if(_currentInventory == _labInventory){
            InventoryDisplayController(_labInventory);
        }
        else if(_currentInventory == _medicinalPlantsInventory){
            
        InventoryDisplayController(_medicinalPlantsInventory);
        }
        else if(_currentInventory == _larvicidePlantInventory){
            InventoryDisplayController(_larvicidePlantInventory);
        }
        else{
            ResetValues();
        }
    }

    public void CloseInventoryPanel(){
        _inventoryPanel.SetActive(false);
        Invoke("HideCursor", 1);
    }
    void HideCursor(){
        Cursor.visible = false;
    }
    void ResetValues(){
        Debug.Log("called");
        _heading.text="0";
        for (int k = 0; k < _itemImages.Length; k++)
        {
            _itemImages[k].sprite = null;
        }
        for (int i = 0; i < _itemImages.Length; i++)
        {
            _itemAmountText[i].text="";
        }
    }

    //WaterAnalysis
    public void ActivateTakeSample(){
        GameObject.FindGameObjectWithTag("water_source_1").GetComponent<BoxCollider>().enabled=true;
        _currentTask.sprite =_emptyTesttube;
         _waterSourceAnalysisPanel.SetActive(false);
    }
    public void OpenResultspane(){
        _resultsPanel.SetActive(true);
    }
    public void HideWaterSourceAnalysisPanel(){
        _waterSourceAnalysisPanel.SetActive(false);
    }
    public void TakeTest(){
        //_currentTask.sprite = none;
    }
}