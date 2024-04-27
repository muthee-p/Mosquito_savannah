using TMPro;
using UnityEngine;

public class LabCollectedItems : MonoBehaviour
{
    [SerializeField] private InventoryObject labInventory;
    [SerializeField] private LabObject lamp, scale, microscpe, roundbottom2, roundbottom3;
    [SerializeField] private GameObject _lamp, _scale, _microscpe, _roundbottom2, _roundbottom3;
     [SerializeField] private AnimationClip clip;
     [SerializeField] private GameObject droppingPanel, reducedLabProcessingPanel;
     [SerializeField] private TextMeshProUGUI reducedTimeText, droppedItemsText;
    private float labprocessingTime;
    private float lampTime, scaleTime, microscpeTime, roundbottom2Time, roundbottom3Time;
    private int droppedItems;
    private AudioSource successSound;


    void Start(){
        successSound= GameObject.Find("successSound").GetComponent<AudioSource>();
    }
  
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            droppingPanel.SetActive(true);
            ShowLabItems();
            CalculateLabProcessingTime();

            Invoke("HideDropPanel", 1.5f);
            Invoke("HideLabProcessingPanel", 1.5f);
        }
    }

    private void ShowLabItems(){
    for (int i = 0; i < labInventory.Container.Count; i++)
    {
        droppedItems += labInventory.Container[i].amount;
        if (lamp && labInventory.Container[i].amount > 0)
        {    
            lampTime = lamp.LabProcessingTime * labInventory.Container[i].amount;      
            if (!_lamp.activeSelf)
            {
                _lamp.SetActive(true); 
            }
        }
        else if (labInventory.Container[i].item == scale && labInventory.Container[i].amount > 0)
        {
            scaleTime= scale.LabProcessingTime * labInventory.Container[i].amount;
            if (!_scale.activeSelf)
            {
                _scale.SetActive(true);
            }
        }
        else if (labInventory.Container[i].item == microscpe && labInventory.Container[i].amount > 0)
        {
            microscpeTime= microscpe.LabProcessingTime * labInventory.Container[i].amount;
            if (!_microscpe.activeSelf)
            {
                _microscpe.SetActive(true);
            }
        }
        else if (labInventory.Container[i].item == roundbottom2 && labInventory.Container[i].amount > 0)
        {
            roundbottom2Time= roundbottom2.LabProcessingTime * labInventory.Container[i].amount;
            if (!_roundbottom2.activeSelf)
            {
                _roundbottom2.SetActive(true);
            }
        }
        else if (labInventory.Container[i].item == roundbottom3 && labInventory.Container[i].amount > 0)
        {
            roundbottom3Time = roundbottom3.LabProcessingTime * labInventory.Container[i].amount;
            if (!_roundbottom3.activeSelf)
            {
                _roundbottom3.SetActive(true);
            }
        }
    }
}

void CalculateLabProcessingTime(){
    labprocessingTime = lampTime + scaleTime + microscpeTime + roundbottom2Time + roundbottom3Time;
    clip.frameRate=+ labprocessingTime;
}
void HideDropPanel(){
    droppingPanel.SetActive(false);
    labInventory.Container.Clear();
    reducedLabProcessingPanel.SetActive(true);
    successSound.Play();
    float reducedtime = (1/clip.frameRate)/60;
    reducedTimeText.text = "Lab Processing Time is now : " + reducedtime;
    droppedItemsText.text = "You dropped : " + droppedItems + "items";
}
void HideLabProcessingPanel(){
    reducedLabProcessingPanel.SetActive(false );
}

}