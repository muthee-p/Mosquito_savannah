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

    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
        DisplayLabInventory();
    }

    void Update(){
        DisplayLabInventory();
    }
    public void OpenInventoryPanel(){
        _inventoryPanel.SetActive(true);
        DisplayLabInventory();
        DisplayMedicinalPlantsInventory();
        DisplayLarvicidePlantInventory();
    }

    public void DisplayLabInventory(){
        _heading.text="Lab Inventory";
        for (int i = 0; i < _labInventory.Container.Count; i++)
        {
            for (int j = 0; j < _itemAmountText.Length; j++)
            {
                _itemAmountText[0].text = _labInventory.Container[0].amount.ToString();
                _itemAmountText[1].text = _labInventory.Container[1].amount.ToString();
            } 
            
            for (int k = 0; k < _itemImages.Length; k++)
            {
               _itemImages[0].sprite =_labInventory.Container[0].sprite;
               _itemImages[1].sprite =_labInventory.Container[1].sprite;
            }
        }
        //labButton.interactable=false;
    }
    public void DisplayMedicinalPlantsInventory(){
         _heading.text="Medicinal Plants Inventory";
        for (int i = 0; i < _medicinalPlantsInventory.Container.Count; i++)
        {
            for (int j = 0; j < _itemAmountText.Length; j++)
            {
                _itemAmountText[0].text = _medicinalPlantsInventory.Container[0].amount.ToString();
                _itemAmountText[1].text = _medicinalPlantsInventory.Container[1].amount.ToString();
            } 
            
            for (int k = 0; k < _itemImages.Length; k++)
            {
               _itemImages[0].sprite =_medicinalPlantsInventory.Container[0].sprite;
               _itemImages[1].sprite = _medicinalPlantsInventory.Container[1].sprite;
            }
        }
        //MedicineButton.interactable = false;
    }
    public void DisplayLarvicidePlantInventory(){
         _heading.text="Larvicide Plants Inventory";
        for (int i = 0; i < _larvicidePlantInventory.Container.Count; i++)
        {
            for (int j = 0; j < _itemAmountText.Length; j++)
            {
                _itemAmountText[0].text = _larvicidePlantInventory.Container[0].amount.ToString();
                _itemAmountText[1].text = _larvicidePlantInventory.Container[1].amount.ToString();
            } 
            
            for (int k = 0; k < _itemImages.Length; k++)
            {
               _itemImages[0].sprite =_larvicidePlantInventory.Container[0].sprite;
               _itemImages[1].sprite =_larvicidePlantInventory.Container[1].sprite;
            }
        }
    }
    public void CloseInventoryPanel(){
        _inventoryPanel.SetActive(false);
        Invoke("HideCursor", 1);
    }
    void HideCursor(){
        Cursor.visible = false;
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