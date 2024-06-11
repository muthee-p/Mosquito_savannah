using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class WaterSourceAnalysisTwo : MonoBehaviour
{ 
    [SerializeField] private WaterSourceCommons waterSourceCommons;
    [SerializeField] private GameObject waterSourcePlane, _watersorceAnalysisPanel, ResultsPanel;
    [SerializeField] private GameObject _takeSample, _disinfectPanel;
    [SerializeField] public TextMeshProUGUI _sampleAmountText, _resultsAmountText, _larvicideAmountText;
    [SerializeField] private TextMeshProUGUI _larvaePercText, _algaePercText, _snailsPercText, _insectPercText, _resultStrength;
    [SerializeField] Image currentTask;
    [SerializeField] GameObject parentTask;
    [SerializeField] private TextMeshProUGUI fuzzyPlantText, plainPlantText, snakePlantText, disinfectedStatus;
    [SerializeField] private TextMeshProUGUI TakeSampleButton, createLarvicideButton, analyseSampleButton;
    [SerializeField] private GameObject _check, checkOne, checkTwo;
    private BoxCollider waterPlaneCollider;
    private Transform player;
    private ResultsGenerator resultsGenerator;
    private int _sampleAmount;
    public int _larvicideAmount;
    public int _resultsAmount;
    private int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;
    private int _larvaePercAmt, _algaePercAmt, _snailsPercAmt, _insectPercAmt;
    public bool functionCalled = false;
    private bool resultsGenerated = false;
    bool isDisinfecting = false;
    bool larvicideCreated = false;
    bool interactable;

    void Start()
    {
        resultsGenerator = ResultsPanel.GetComponent<ResultsGenerator>();
        waterPlaneCollider = waterSourcePlane.GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disinfectedStatus.text = "Untested";
        if (_resultsAmount == 0)
        {
            fuzzyPlantText.text = "N/A";
            plainPlantText.text = "N/A";
            snakePlantText.text = "N/A";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = true; 
            //ResetText();
            UpdateUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideWaterSourceAnalysisPanel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && interactable){
            _watersorceAnalysisPanel.SetActive(true);
        }
            if (_watersorceAnalysisPanel.activeSelf)
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_sampleAmount == 0 && _resultsAmount == 0 && _larvicideAmount == 0)
                {
                    waterSourceCommons.TakeSample(waterSourcePlane, currentTask, _watersorceAnalysisPanel);
                    _sampleAmountText.text = "Sample = " + _sampleAmount;
                    parentTask.SetActive(true);
                }
            }
            if (_sampleAmount == 0 && _resultsAmount == 0 && _larvicideAmount == 0)
            {
                TakeSampleButton.color = Color.green;
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
                if (!larvicideCreated)
                {
                    waterSourceCommons.ClickCreateLarvicideButton(currentTask,
                 _larvicideAmount, _larvicideAmountText, _resultsAmount, waterSourcePlane,
                 plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount,
                 _resultsAmountText, isDisinfecting, fuzzyPlantText, plainPlantText, snakePlantText, larvicideCreated, checkOne, checkTwo, _check);

                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                HideWaterSourceAnalysisPanel();
            }
        }
    }

    public void WaterPanel()
    {
        if (currentTask.sprite == waterSourceCommons.larvicideSprite || isDisinfecting)
        {
            _disinfectPanel.SetActive(true);
            Invoke("UpdateLarvicideApplication", 1.5f);
        }
        else if (!isDisinfecting)
        {
            _takeSample.SetActive(true);

            currentTask.sprite = waterSourceCommons._fullTestTube;

            _sampleAmount++;
            _sampleAmountText.text = "Sample = " + _sampleAmount;
            TakeSampleButton.color = Color.white;
            analyseSampleButton.color = Color.green;
        }
        Invoke("HidePanel", 1.5f);
    }

    public void HideWaterSourceAnalysisPanel()
    {
        _watersorceAnalysisPanel.SetActive(false);
        interactable = false;
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
            fuzzyPlantText, plainPlantText, snakePlantText,
            plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );
        currentTask.sprite = waterSourceCommons.clipboard;
        analyseSampleButton.color = Color.white;
        createLarvicideButton.color = Color.green;
    }


    private void HidePanel()
    {
        if (_disinfectPanel.activeSelf)
        {
            _disinfectPanel.SetActive(false);
            _larvicideAmount--;
            createLarvicideButton.color = Color.white;
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
        imagecolor.a = 0.4f;
        currentTask.color = imagecolor;
        waterSourceCommons.successSound.Play();
        functionCalled = true;
    }

    private void UpdateUI()
    {
        waterSourceCommons.TextUpdate(
            _resultsAmount, fuzzyPlantText, plainPlantText, snakePlantText,
            disinfectedStatus, _sampleAmount, functionCalled, plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount
        );
    }
}