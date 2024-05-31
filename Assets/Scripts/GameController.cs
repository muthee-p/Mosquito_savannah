using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _waterSourceAnalysisPanel, _resultsPanel, _instructionsPanel;
    [SerializeField] private InventoryObject _labInventory, _medicinalPlantsInventory, _larvicidePlantInventory;
    [SerializeField] private Image[] _itemImages;
    [SerializeField] private TextMeshProUGUI[] _itemAmountText;
    [SerializeField] private TextMeshProUGUI _heading;
    [SerializeField] Button labButton, MedicineButton, larvicideButton;
    [SerializeField] Image _currentTask;
    [SerializeField] Sprite _emptySprite;
    private InventoryObject _currentInventory;
   

    void Start(){
        _currentInventory=_labInventory;
        labButton.interactable= false;
    }

    void Update(){
        InventoryButtonsController();
        if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventoryPanel();
            }
        if (Input.GetKeyDown(KeyCode.L))
            {
                DisplayLabInventory();
            }
        if (Input.GetKeyDown(KeyCode.N))
            {
                 DisplayMedicinalPlantsInventory();
            }
        if (Input.GetKeyDown(KeyCode.K))
            {
                DisplayLarvicidePlantInventory();
            }
        if (Input.GetKeyDown(KeyCode.X))
            {
                 CloseInventoryPanel();
            }
        if (Input.GetKeyDown(KeyCode.H))
            {
                 OpenInstructions();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Exit();
            }
    }
    public void OpenInventoryPanel(){
        _inventoryPanel.SetActive(true);
        _currentInventory=_labInventory;
    }

    public void DisplayLabInventory()
    {
        ResetValues();
        _currentInventory=_labInventory;
    }

    public void DisplayMedicinalPlantsInventory()
    {
        ResetValues();
        _currentInventory=_medicinalPlantsInventory;
        labButton.interactable= true;
    }

    public void DisplayLarvicidePlantInventory()
    {
        
        ResetValues();
        _currentInventory=_larvicidePlantInventory;
        labButton.interactable= true;
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
             _heading.text = "Lab Inventory";
            InventoryDisplayController(_labInventory);
        }
        else if(_currentInventory == _medicinalPlantsInventory){
             _heading.text = "Medicinal Plants Inventory";
        InventoryDisplayController(_medicinalPlantsInventory);
        }
        else if(_currentInventory == _larvicidePlantInventory){
            _heading.text = "Larvicide Plants Inventory";
            InventoryDisplayController(_larvicidePlantInventory);
        }
        else{
            _heading.text = "Inventory";
            ResetValues();
        }
    }

    public void CloseInventoryPanel(){
        _inventoryPanel.SetActive(false);
    }
   
    void ResetValues(){
       
        _heading.text="0";
        for (int k = 0; k < _itemImages.Length; k++)
        {
            _itemImages[k].sprite = _emptySprite;
        }
        for (int i = 0; i < _itemImages.Length; i++)
        {
            _itemAmountText[i].text="";
        }
    }
public void OpenInstructions(){
     _instructionsPanel.SetActive(!_instructionsPanel.activeSelf);
}
public void Exit(){
    Application.Quit();
}
    
}