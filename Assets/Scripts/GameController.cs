using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _waterSourceAnalysisPanel, _resultsPanel, _instructionsPanel, loadingScreen;
    [SerializeField] private InventoryObject _labInventory, _medicinalPlantsInventory, _larvicidePlantInventory;
    [SerializeField] private Image[] _itemImages;
    [SerializeField] private TextMeshProUGUI[] _itemAmountText;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private TextMeshProUGUI _heading;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material nightSkyBox, daySkybox;
    [SerializeField] Button labButton, MedicineButton, larvicideButton;
    [SerializeField] Image _currentTask;
    [SerializeField] Sprite _emptySprite;
    private InventoryObject _currentInventory;
    public float rotationSpeed = 1.0f;
    public float nightIntensity = 0.3f;
    public float dayIntensity = 1f;
    public float transitionSpeed = 1.0f;
    private float currentIntensity;

    public float dayFogDensity = 0.003f;
    public float nightFogDensity = 0.0003f;
    private float currentFogDensity;

    void Start(){
        _currentInventory=_labInventory;
        labButton.interactable= false;
        Invoke("HideLoadingScreen", 5);
        currentIntensity = RenderSettings.skybox.GetFloat("_Exposure");
        currentFogDensity = RenderSettings.fogDensity;

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

        float xRotation = directionalLight.transform.eulerAngles.x;

        if (xRotation >= 240f && xRotation <= 320f)
        {
            RenderSettings.skybox = nightSkyBox;
        }
        else
        {
            RenderSettings.skybox = daySkybox;
        }

        // Rotate the skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        float targetIntensity;
        if (xRotation >= 180f && xRotation <= 360f)
        {
            targetIntensity = nightIntensity;
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
        }
        else
        {
            targetIntensity = dayIntensity;
            foreach (var light in lights)
            {
                light.SetActive(false);
            }
        }

        // Smoothly transition to the target intensity
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * transitionSpeed);
        RenderSettings.skybox.SetFloat("_Exposure", currentIntensity);

        float targetFogDensity = Mathf.Lerp(nightFogDensity, dayFogDensity, Mathf.InverseLerp(270f, 90f, xRotation));

        // Smoothly transition to the target fog density
        currentFogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, Time.deltaTime * transitionSpeed);
        RenderSettings.fogDensity = currentFogDensity;
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
    void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }
    
}