using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class WaterSourceAnalysis : MonoBehaviour
{
    [SerializeField] private WaterSourceAnalysisOne waterSourceAnalysisOne;
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
        waterSourceAnalysisOne.enabled = false;
        resultsGenerator = ResultsPanel.GetComponent<ResultsGenerator>();
        waterPlaneCollider = waterSourcePlane.GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disinfectedStatus.text = "Untested";
        ResetRecommendedAmounts();
    }

    private void ResetRecommendedAmounts()
    {
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
            UpdateUI();
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
        if (_watersorceAnalysisPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_sampleAmount == 0 && _resultsAmount == 0 && _larvicideAmount == 0)
                {
                    waterSourceCommons.TakeSample(waterSourcePlane, currentTask, _watersorceAnalysisPanel);
                    _sampleAmountText.text = "Sample = " + _sampleAmount;
                }
            }

          
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_sampleAmount == 1 && _resultsAmount == 0 && resultsGenerated == false)
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
            parentTask.SetActive(true);
            currentTask.sprite = waterSourceCommons._fullTestTube;
            
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
        waterSourceCommons.AnalysisComplete(
            _resultsAmount, _resultsAmountText, _sampleAmount, _sampleAmountText,
            fuzzyPlantText, plainPlantText, snakePlantText, createLarvicideButton,
            plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount, canMakeLarvicide
        );
    }
    public void ClickCreateLarvicideButton()
    {
       
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

    private void UpdateUI()
    {
        waterSourceCommons.TextUpdate(
            _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText,
            disinfectedStatus, _sampleAmount, analyseSampleButton, createLarvicideButton,
            functionCalled, canMakeLarvicide, plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );
    }
}