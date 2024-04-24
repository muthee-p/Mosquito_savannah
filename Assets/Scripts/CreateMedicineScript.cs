using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateMedicineScript: MonoBehaviour {
  
   [SerializeField] private GameObject _createMedicinePanel;
   [SerializeField] private InventoryObject _medicinalPlantsInventory;
   [SerializeField] private TextMeshProUGUI _lavenderText, _redheadText, _glowingText, _orangeflowerText, _medicineAmountText;
   [SerializeField] private  CurePlantObject lavenderPlant, redheadPlant, glowingPlant, orangeFlowerPlant;
   [SerializeField] private Button _createMedicineButton, _sendMedicineButton;
    private int _lavenderAmount, _redheadAmount, _glowingAmount, _orangeflowerAmount, _medicineAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _createMedicinePanel.SetActive(true);
            CheckPlantAmount();
            ActivateCreateMedicineButton();
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
        _medicineAmount++;
        _medicineAmountText.text = "Medicine Amount = "+_medicineAmount;
        
        if(_medicineAmount<= 1){
            _sendMedicineButton.interactable=true;
        }
    }
}