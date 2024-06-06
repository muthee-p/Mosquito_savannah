using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WaterSourceCommons : MonoBehaviour
{
    [SerializeField] private InventoryObject larvicideInventory;
    [SerializeField] private LarvicidePlantObject fuzzyPlant, plainPlant, snakePlant;
    public Sprite larvicideSprite, _fullTestTube;
    [SerializeField] public Sprite _emptyTesttube, _emptySprite, medicineSprite;
   
    public AudioSource successSound, collectSound;
    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;
    private string status;

    public void TextUpdate(
        int _resultsAmount, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText, TextMeshProUGUI disinfectedStatus, int _sampleAmount,
        bool functionCalled, int plainPlantRecommendedAmount, int snakePlantRecommendedAmount,
        int fuzzyPlantRecommendedAmount)
    {
        if (disinfectedStatus.text == "Disinfected")
        {
            disinfectedStatus.color = Color.green;
        }
        if (functionCalled)
        {
            disinfectedStatus.text = "Disinfected";
            disinfectedStatus.color = Color.green;
        }
       
        CheckPlantAmount(
            _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText, plainPlantRecommendedAmount,
            snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );

        CreateResults(_sampleAmount, disinfectedStatus);
    }

    public void AnalysisComplete(
        int _resultsAmount, TextMeshProUGUI _resultsAmountText, int _sampleAmount,
        TextMeshProUGUI _sampleAmountText, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText, int plainPlantRecommendedAmount,
        int snakePlantRecommendedAmount, int fuzzyPlantRecommendedAmount)
    {
        collectSound.Play();
        _resultsAmount++;
        _resultsAmountText.text = "Results = " + _resultsAmount;
        _sampleAmount--;
        _sampleAmountText.text = "Sample = " + _sampleAmount;
        CheckPlantAmount(
            _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText,
            plainPlantRecommendedAmount,
            snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );
    }


    public void CheckPlantAmount(
        int _resultsAmount, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText,
        int plainPlantRecommendedAmount, int snakePlantRecommendedAmount, int fuzzyPlantRecommendedAmount)
    {
        for (int i = 0; i < larvicideInventory.Container.Count; i++)
        {
            if (larvicideInventory.Container[i].item == fuzzyPlant)
            {
                fuzzyPlant_amt = larvicideInventory.Container[i].amount;
                fuzzyPlantText.text = fuzzyPlant_amt + " / " + fuzzyPlantRecommendedAmount;
            }
            else if (larvicideInventory.Container[i].item == plainPlant)
            {
                plainPlant_amt = larvicideInventory.Container[i].amount;
                plainPlantText.text = plainPlant_amt + " / " + plainPlantRecommendedAmount;
            }
            else if (larvicideInventory.Container[i].item == snakePlant)
            {
                snakePlant_amt = larvicideInventory.Container[i].amount;
                snakePlantText.text = snakePlant_amt + " / " + snakePlantRecommendedAmount;
            }
        }
        if (fuzzyPlant_amt <= fuzzyPlantRecommendedAmount)fuzzyPlantText.color = Color.red;
        if(plainPlant_amt <= plainPlantRecommendedAmount)plainPlantText.color = Color.red;
        if(snakePlant_amt <= snakePlantRecommendedAmount)snakePlantText.color = Color.red;

        if (larvicideInventory.Container.Count == 0)
        {
            fuzzyPlantText.text = "0 / " + fuzzyPlantRecommendedAmount;
            plainPlantText.text = "0 / " + plainPlantRecommendedAmount;
            snakePlantText.text = "0 / " + snakePlantRecommendedAmount;
        }
    }
    public void ClickCreateLarvicideButton(Image currentTask, int _larvicideAmount, TextMeshProUGUI _larvicideAmountText, 
                int _resultsAmount, GameObject waterSourcePlane, int plainPlantRecommendedAmount, 
                int snakePlantRecommendedAmount, int fuzzyPlantRecommendedAmount, TextMeshProUGUI _resultsAmountText,
                bool isDisinfecting, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
                TextMeshProUGUI snakePlantText, bool larvicideCreated, GameObject checkOne, GameObject checkTwo, GameObject _check)
    {
        if (fuzzyPlant_amt >= fuzzyPlantRecommendedAmount && plainPlant_amt >= plainPlantRecommendedAmount 
            && snakePlant_amt >= snakePlantRecommendedAmount && 
            fuzzyPlantRecommendedAmount>0 && plainPlantRecommendedAmount>0 && snakePlantRecommendedAmount>0)
        {
            larvicideCreated = true;
            collectSound.Play();
            currentTask.sprite = larvicideSprite;
            _larvicideAmount++;
            _larvicideAmountText.text = "Larvicide = " + _larvicideAmount;
            isDisinfecting = true;
            checkOne.SetActive(true);
            checkTwo.SetActive(true);
            _check.SetActive(true);
            ApplyLarvicide(waterSourcePlane,plainPlantRecommendedAmount, snakePlantRecommendedAmount, 
                fuzzyPlantRecommendedAmount, fuzzyPlantText, plainPlantText, snakePlantText, _larvicideAmount, _larvicideAmountText);
        }
       
    }


    public void TakeSample(GameObject waterSourcePlane, Image currentTask, GameObject _watersorceAnalysisPanel)
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        currentTask.sprite = _emptyTesttube;
        _watersorceAnalysisPanel.SetActive(false);
        collectSound.Play();
    }

    
    public void CreateResults(int _sampleAmount, TextMeshProUGUI disinfectedStatus)
    {
        if (_sampleAmount >= 1)
        {
            disinfectedStatus.text = "Untreated";
            disinfectedStatus.color = Color.yellow;
        }
    }

    public void ApplyLarvicide(GameObject waterSourcePlane, int plainPlantRecommendedAmount,
        int snakePlantRecommendedAmount, int fuzzyPlantRecommendedAmount, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText, int _larvicideAmount, TextMeshProUGUI _larvicideAmountText)
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        _larvicideAmount--;
        _larvicideAmountText.text = "Larvicide = " + _larvicideAmount;
        for (int i = 0; i < larvicideInventory.Container.Count; i++)
        {
            if (larvicideInventory.Container[i].item == fuzzyPlant)
            {
                larvicideInventory.Container[i].amount -= fuzzyPlantRecommendedAmount;
                fuzzyPlant_amt = larvicideInventory.Container[i].amount;
                fuzzyPlantText.text = fuzzyPlant_amt + " / " + fuzzyPlantRecommendedAmount;
            }
            else if (larvicideInventory.Container[i].item == plainPlant)
            {
                larvicideInventory.Container[i].amount -= plainPlantRecommendedAmount;
                plainPlant_amt = larvicideInventory.Container[i].amount;
                plainPlantText.text = plainPlant_amt + " / " + plainPlantRecommendedAmount;
            }
            else if (larvicideInventory.Container[i].item == snakePlant)
            {
                larvicideInventory.Container[i].amount -= snakePlantRecommendedAmount;
                snakePlant_amt = larvicideInventory.Container[i].amount;
                snakePlantText.text = snakePlant_amt + " / " + snakePlantRecommendedAmount;
            }
        }
    }

}