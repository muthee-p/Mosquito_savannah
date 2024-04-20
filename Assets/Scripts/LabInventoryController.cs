using TMPro;
using UnityEngine;

public class LabInventoryController: MonoBehaviour{
    [SerializeField] private TextMeshProUGUI rounded_flask_2_text, round_flask_3_text;
    [SerializeField] private InventoryObject labInventory;

    void Start(){
        GetAmounts() ;
    }
    void Update(){
        GetAmounts();
    }

    void GetAmounts(){
        for (int i = 0; i < labInventory.Container.Count; i++)
        {
            rounded_flask_2_text.text = labInventory.Container[0].amount.ToString("n0");
            round_flask_3_text.text = labInventory.Container[2].amount.ToString("n0");
        }
        
    }

}


