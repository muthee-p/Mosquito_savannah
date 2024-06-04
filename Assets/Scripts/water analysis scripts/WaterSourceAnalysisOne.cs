using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class WaterSourceAnalysisOne : MonoBehaviour
{
    [SerializeField] private WaterSourceAnalysis waterSourceAnalysis;
    [SerializeField] private WaterSourceCommons waterSourceCommons;
    [SerializeField] private GameObject waterSourcePlane, _watersorceAnalysisPanel, ResultsPanel;
    [SerializeField] private GameObject _takeSample, _disinfectPanel;
    [SerializeField] public TextMeshProUGUI _resultsAmountText, _larvicideAmountText;
    [SerializeField] public TextMeshProUGUI _sampleAmountText;
    [SerializeField] private TextMeshProUGUI _larvaePercText, _algaePercText, _snailsPercText, _insectPercText;
    [SerializeField] private TextMeshProUGUI _resultStrength;
    [SerializeField] Image currentTask;
    [SerializeField] GameObject parentTask;
    [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText;
    [SerializeField] private Button createLarvicideButton, analyseSampleButton;
    [SerializeField] public Button TakeSampleButton;
    [SerializeField] public TextMeshProUGUI disinfectedStatus;
    private BoxCollider waterPlaneCollider;
    private Transform player;
    private ResultsGenerator resultsGenerator;
    private int _sampleAmount;
    public int _larvicideAmount;
    public int _resultsAmount;
    private int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;
    private int _larvaePercAmt, _algaePercAmt, _snailsPercAmt, _insectPercAmt;
    public bool functionCalled = false;
    public bool canMakeLarvicide = false;
    private bool resultsGenerated = false;

    void Start()
    {
        waterSourceAnalysis.enabled = false;
        resultsGenerator = ResultsPanel.GetComponent<ResultsGenerator>();
        waterPlaneCollider = waterSourcePlane.GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disinfectedStatus.text = "Untested";
        plainPlantRecommendedAmount = 0;
        snakePlantRecommendedAmount = 0;
        fuzzyPlantRecommendedAmount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sampleAmountText.text = "Sample = " + _sampleAmount;
            _watersorceAnalysisPanel.SetActive(true);
            if (_resultsAmount == 0)
            {
                fuzzyPlantText.text = "N/A";
                plainPlantText.text = "N/A";
                snakePlantText.text = "N/A";
            }
            waterSourceCommons.TextUpdate(_resultsAmount, fuzzyPlantText, plainPlantText,
                             snakePlantText, disinfectedStatus, _sampleAmount, analyseSampleButton
                             , createLarvicideButton, functionCalled, canMakeLarvicide,
                             plainPlantRecommendedAmount, snakePlantRecommendedAmount,
                             fuzzyPlantRecommendedAmount);
            Debug.Log("plain " + plainPlantRecommendedAmount + "snake " + snakePlantRecommendedAmount + "Fuzzy" + fuzzyPlantRecommendedAmount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(false);
        }
    }
    void Update()
    {
        if (_watersorceAnalysisPanel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_sampleAmount == 0)
                {
                    waterSourceCommons.TakeSample(waterSourcePlane, currentTask, _watersorceAnalysisPanel);
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_sampleAmount == 1 && _resultsAmount == 0)
                {
                    AnalyseSample();
                }
            }
            if (Input.GetKeyDown(KeyCode.V))
            {

                ClickCreateLarvicideButton();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                HideWaterSourceAnalysisPanel();
            }

        }

    }

    public void WaterPanel()
    {
        if (currentTask.sprite == waterSourceCommons.larvicideSprite)
        {
            _disinfectPanel.SetActive(true);
            Invoke("UpdateLarvicideApplication", 1.5f);
        }
        else
        {
            _takeSample.SetActive(true);

            currentTask.sprite = waterSourceCommons._fullTestTube;
            parentTask.SetActive(true);
            _sampleAmount++;

            TakeSampleButton.interactable = false;
        }

        Invoke("HidePanel", 1.5f);
    }


    public void HideWaterSourceAnalysisPanel()
    {
        _watersorceAnalysisPanel.SetActive(false);
    }
    public void AnalyseSample()
    {
        if (_sampleAmount >= 1)
        {
            ResultsPanel.GetComponent<Animator>().SetTrigger("processing");
            resultsGenerator.GenerateResults(
                _larvaePercText, _algaePercText, _snailsPercText, _insectPercText, _resultStrength,
                ref plainPlantRecommendedAmount, ref snakePlantRecommendedAmount, ref fuzzyPlantRecommendedAmount,
                ref _larvaePercAmt, ref _algaePercAmt, ref _snailsPercAmt, ref _insectPercAmt, ref resultsGenerated
            );
            Invoke("AnalysisComplete", 4f);
        }
    }
    public void AnalysisComplete()
    {
        waterSourceCommons.AnalysisComplete(_resultsAmount, _resultsAmountText, _sampleAmount, _sampleAmountText,
                                 fuzzyPlantText, plainPlantText, snakePlantText, createLarvicideButton, plainPlantRecommendedAmount,
                                snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount, canMakeLarvicide);
    }
    public void ClickCreateLarvicideButton()
    {
        Debug.Log(canMakeLarvicide);
        if (canMakeLarvicide)
        {
            waterSourceCommons.collectSound.Play();
            currentTask.sprite = waterSourceCommons.larvicideSprite;
            _larvicideAmount++;
            _larvicideAmountText.text = "Larvicide = " + _larvicideAmount;
            _resultsAmount--;
            waterSourceCommons.ApplyLarvicide(waterSourcePlane);
        }
    }
    private void HidePanel()
    {
        if (_disinfectPanel.activeSelf)
        {
            _disinfectPanel.SetActive(false);
            _larvicideAmount--;
            TakeSampleButton.interactable = true;
        }
        else
        {
            _takeSample.SetActive(false);
        }

        waterSourcePlane.GetComponent<BoxCollider>().enabled = false;
    }
    public void UpdateLarvicideApplication()
    {
        currentTask.sprite = waterSourceCommons.medicineSprite;
        Color imagecolor = currentTask.color;
        imagecolor.a = 60;
        waterSourceCommons.successSound.Play();
        functionCalled = true;
    }
}

/*using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;


public class WaterSourceAnalysisOne : MonoBehaviour
{
    [SerializeField] private WaterSourceCommons waterSourceCommons;
    [SerializeField] private GameObject waterSourcePlane, _watersorceAnalysisPanel, ResultsPanel;
    [SerializeField] private GameObject _takeSample, _disinfectPanel;
    [SerializeField] public TextMeshProUGUI _resultsAmountText, _larvicideAmountText;
    [SerializeField] public TextMeshProUGUI _sampleAmountText;
    [SerializeField] private Image fuzzyPlantImage, plainPlantImage, snakePlantImage;
    [SerializeField] private TextMeshProUGUI _larvaePercText, _algaePercText, _snailsPercText, _insectPercText;
    [SerializeField] private TextMeshProUGUI _resultStrength;
    [SerializeField] Image currentTask;
    [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText;
    [SerializeField] private Button createLarvicideButton, analyseSampleButton;
    [SerializeField] public Button TakeSampleButton;
    [SerializeField] public TextMeshProUGUI disinfectedStatus;
    private string status;

    private BoxCollider waterPlaneCollider;
    private Transform player;
    private ResultsGenerator resultsGenerator;
    private int _sampleAmount;
    public int _larvicideAmount;
    public int _resultsAmount;
    private int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;
    private int _larvaePercAmt, _algaePercAmt, _snailsPercAmt, _insectPercAmt;
    public bool functionCalled = false;
    public bool canMakeLarvicide = false;

    void Start()
    {
        resultsGenerator = ResultsPanel.GetComponent<ResultsGenerator>();
        waterPlaneCollider = waterSourcePlane.GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disinfectedStatus.text = "Untested";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sampleAmountText.text = "Sample = " + _sampleAmount;

            _watersorceAnalysisPanel.SetActive(true);
            TextUpdate();
            CheckPlantAmount();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _watersorceAnalysisPanel.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeSample();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AnalyseSample();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ClickCreateLarvicideButton();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            HideWaterSourceAnalysisPanel();
        }
    }
    private void HideWaterSourceAnalysisPanel()
    {
        _watersorceAnalysisPanel.SetActive(false);
    }
    private void TakeSample()
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        currentTask.sprite = waterSourceCommons._emptyTesttube;
        _watersorceAnalysisPanel.SetActive(false);
        waterSourceCommons.collectSound.Play();
    }

    public void WaterPanel()
    {
        if (currentTask.sprite == waterSourceCommons.larvicideSprite)
        {
            _disinfectPanel.SetActive(true);
            Invoke("UpdateLarvicideApplication", 1.5f);
        }
        else
        {
            _takeSample.SetActive(true);
            currentTask.sprite = waterSourceCommons._fullTestTube;
            _sampleAmount++;

            TakeSampleButton.interactable = false;
        }
        Invoke("HidePanel", 1.5f);
    }
    private void HidePanel()
    {
        if (_disinfectPanel.activeSelf)
        {
            _disinfectPanel.SetActive(false);
            _larvicideAmount--;
            TakeSampleButton.interactable = true;
        }
        else
        {
            _takeSample.SetActive(false);
        }

        waterPlaneCollider.enabled = false;
    }
    public void CreateResults()
    {
        if (_sampleAmount >= 1)
        {
            analyseSampleButton.interactable = true;
            disinfectedStatus.text = "Untreated";
            disinfectedStatus.color = Color.yellow;
        }
    }

    private void AnalyseSample()
    {
        if (_sampleAmount >= 1)
        {
            ResultsPanel.GetComponent<Animator>().SetTrigger("processing");
            resultsGenerator.GenerateResults(_larvaePercText, _algaePercText, _snailsPercText, _insectPercText, _resultStrength, plainPlantRecommendedAmount,
                                snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount, _larvaePercAmt,
                                _algaePercAmt, _snailsPercAmt, _insectPercAmt);

            Invoke("AnalysisComplete", 4f);
        }
    }

    public void UpdateLarvicideApplication()
    {
        currentTask.sprite = waterSourceCommons._emptySprite;
        waterSourceCommons.successSound.Play();
        functionCalled = true;
    }

    public void TextUpdate()
    {
        if (_resultsAmount == 0)
        {
            fuzzyPlantText.text = "N/A";
            plainPlantText.text = "N/A";
            snakePlantText.text = "N/A";
        }
        if (disinfectedStatus.text == "Disinfected")
        {

            disinfectedStatus.color = Color.green;
        }
        if (functionCalled == true)
        {
            disinfectedStatus.text = "Disinfected";
            disinfectedStatus.color = Color.green;
        }
        CheckPlantAmount();
        CreateResults();
    }

    private int fuzzyPlant_amt, plainPlant_amt, snakePlant_amt;
    public void CheckPlantAmount()
    {
        if (_resultsAmount >= 1)
        {
            for (int i = 0; i < waterSourceCommons.larvicideInventory.Container.Count; i++)
            {
                if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.fuzzyPlant)
                {
                    fuzzyPlant_amt = waterSourceCommons.larvicideInventory.Container[i].amount;
                    fuzzyPlantImage.sprite = waterSourceCommons.larvicideInventory.Container[i].sprite;
                    fuzzyPlantText.text = fuzzyPlant_amt.ToString() + " /1";
                }
                if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.plainPlant)
                {
                    plainPlant_amt = waterSourceCommons.larvicideInventory.Container[i].amount;
                    plainPlantImage.sprite = waterSourceCommons.plainPlant.sprite;
                    plainPlantText.text = plainPlant_amt.ToString() + " /2";
                }
                if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.snakePlant)
                {
                    snakePlant_amt = waterSourceCommons.larvicideInventory.Container[i].amount;
                    snakePlantImage.sprite = waterSourceCommons.snakePlant.sprite;
                    snakePlantText.text = snakePlant_amt.ToString() + " /3";
                }
            }
            if (fuzzyPlant_amt >= 1 && plainPlant_amt >= 2 && snakePlant_amt >= 3)
            {
                createLarvicideButton.interactable = true;
                canMakeLarvicide = true;
            }
        }
    }

    public void AnalysisComplete()
    {
        waterSourceCommons.collectSound.Play();
        _resultsAmount++;
        _resultsAmountText.text = "Results = " + _resultsAmount;
        _sampleAmount--;
        _sampleAmountText.text = "Sample= " + _sampleAmount--;
        CheckPlantAmount();
        TextUpdate();
    }
    public void ClickCreateLarvicideButton()
    {

        if (canMakeLarvicide == true)
        {
            waterSourceCommons.collectSound.Play();
            currentTask.sprite = waterSourceCommons.larvicideSprite;
            _larvicideAmount++;
            _larvicideAmountText.text = "Larvicide = " + _larvicideAmount++;
            _resultsAmount--;
            ApplyLarvicide();
        }
    }
    public void ApplyLarvicide()
    {
        waterSourcePlane.GetComponent<BoxCollider>().enabled = true;
        for (int i = 0; i < waterSourceCommons.larvicideInventory.Container.Count; i++)
        {
            if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.fuzzyPlant)
            {
                waterSourceCommons.larvicideInventory.Container[i].amount -= 1;
            }
            if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.plainPlant)
            {
                waterSourceCommons.larvicideInventory.Container[i].amount -= 2;
            }
            if (waterSourceCommons.larvicideInventory.Container[i].item == waterSourceCommons.snakePlant)
            {
                waterSourceCommons.larvicideInventory.Container[i].amount -= 3;
            }
        }
    }

}*/