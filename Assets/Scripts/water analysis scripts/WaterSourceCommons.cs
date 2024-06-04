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
        Button analyseSampleButton, Button createLarvicideButton, bool functionCalled,
        bool canMakeLarvicide, int plainPlantRecommendedAmount, int snakePlantRecommendedAmount,
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
        else if (_resultsAmount > 0)
        {
            CheckPlantAmount(
                _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText,
                createLarvicideButton, canMakeLarvicide, plainPlantRecommendedAmount,
                snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
            );
        }
        CreateResults(_sampleAmount, analyseSampleButton, disinfectedStatus);
    }

    public void AnalysisComplete(
        int _resultsAmount, TextMeshProUGUI _resultsAmountText, int _sampleAmount,
        TextMeshProUGUI _sampleAmountText, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText, Button createLarvicideButton, int plainPlantRecommendedAmount,
        int snakePlantRecommendedAmount, int fuzzyPlantRecommendedAmount, bool canMakeLarvicide)
    {
        collectSound.Play();
        _resultsAmount++;
        _resultsAmountText.text = "Results = " + _resultsAmount;
        _sampleAmount--;
        _sampleAmountText.text = "Sample = " + _sampleAmount;
        CheckPlantAmount(
            _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText,
            createLarvicideButton, canMakeLarvicide, plainPlantRecommendedAmount,
            snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );
    }

    public void CheckPlantAmount(
        int _resultsAmount, TextMeshProUGUI fuzzyPlantText, TextMeshProUGUI plainPlantText,
        TextMeshProUGUI snakePlantText, Button createLarvicideButton, bool canMakeLarvicide,
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
        if (larvicideInventory.Container.Count == 0)
        {
            fuzzyPlantText.text = "0 / " + fuzzyPlantRecommendedAmount;
            plainPlantText.text = "0 / " + plainPlantRecommendedAmount;
            snakePlantText.text = "0 / " + snakePlantRecommendedAmount;
        }

        if (fuzzyPlant_amt >= fuzzyPlantRecommendedAmount && plainPlant_amt >= plainPlantRecommendedAmount && snakePlant_amt >= snakePlantRecommendedAmount)
        {
            createLarvicideButton.interactable = true;
            canMakeLarvicide = true;
        }
        Debug.Log(canMakeLarvicide);
    }

   
    public void TakeSample(GameObject waterSourcePlane, Image currentTask, GameObject _watersorceAnalysisPanel)
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        currentTask.sprite = _emptyTesttube;
        _watersorceAnalysisPanel.SetActive(false);
        collectSound.Play();
    }

    
    public void CreateResults(int _sampleAmount, Button analyseSampleButton, TextMeshProUGUI disinfectedStatus)
    {
        if (_sampleAmount >= 1)
        {
            analyseSampleButton.interactable = true;
            disinfectedStatus.text = "Untreated";
            disinfectedStatus.color = Color.yellow;
        }
    }

    public void ApplyLarvicide(GameObject waterSourcePlane)
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        for (int i = 0; i < larvicideInventory.Container.Count; i++)
        {
            if (larvicideInventory.Container[i].item == fuzzyPlant)
            {
                larvicideInventory.Container[i].amount -= 1;
            }
            if (larvicideInventory.Container[i].item == plainPlant)
            {
                larvicideInventory.Container[i].amount -= 2;
            }
            if (larvicideInventory.Container[i].item == snakePlant)
            {
                larvicideInventory.Container[i].amount -= 3;
            }
        }
    }

}