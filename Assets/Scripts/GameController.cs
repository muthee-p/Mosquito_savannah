using System;
using System.Collections;
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
    [SerializeField] private Material nightSkybox, daySkybox;
    [SerializeField] Button labButton, MedicineButton, larvicideButton;
    [SerializeField] Image _currentTask;
    [SerializeField] Sprite _emptySprite;
    private InventoryObject _currentInventory;
    private float xRotation;
    public float rotationSpeed = 1.0f;
    public float nightIntensity = 0.3f;
    public float dayIntensity = 1f;
    public float transitionSpeed = 1.0f;
    private float currentIntensity;
    float currentRotation;

    public float dayFogDensity = 0.003f;
    public float nightFogDensity = 0.0003f;
    private float currentFogDensity;

    private Color eveningColor = new Color(0x59 / 255f, 0x55 / 255f, 0x4A / 255f); // #EEC34E
    private Color nightColor = new Color(0x04 / 255f, 0x06 / 255f, 0x10 / 255f);//040610
    private Color morningColor = new Color(0xB0 / 255f, 0xA7 / 255f, 0x8F / 255f); // #F1DBA0
    private Color earlyMorningColor = new Color(0x87 / 255f, 0x87 / 255f, 0x87 / 255f); // #8E8E8E

    private Material currentSkybox;


    void Start(){
        _currentInventory=_labInventory;
        labButton.interactable= false;
        Invoke("HideLoadingScreen", 5);
       
        currentIntensity = RenderSettings.skybox.GetFloat("_Exposure");

        currentFogDensity = RenderSettings.fogDensity;
        currentSkybox = RenderSettings.skybox;
        StartCoroutine(UpdateSkyboxSettings());
    }

    void Update()
    {
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
        currentSkybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        currentRotation += Time.deltaTime * rotationSpeed;
        currentSkybox.SetFloat("_Rotation", currentRotation);

        foreach (var light in lights)
            {
            xRotation = directionalLight.transform.eulerAngles.x;
            if (xRotation >= 200f && xRotation <= -30f)
                {
                    light.SetActive(true);
                }else light.SetActive(false);

        }
    }

    IEnumerator UpdateSkyboxSettings()
    {
        while (true)
        {
            xRotation = directionalLight.transform.eulerAngles.x;
            // Normalize xRotation
            if (xRotation > 180f)
            {
                xRotation -= 360f;
            }

            // Determine which skybox to use
            if (xRotation >= -130f && xRotation <= -50f)
            {
                if (RenderSettings.skybox != nightSkybox)
                {
                    RenderSettings.skybox = nightSkybox;
                    currentSkybox = nightSkybox;
                    currentSkybox.SetFloat("_Rotation", currentRotation);
                }
            }
            else
            {
                if (RenderSettings.skybox != daySkybox)
                {
                    RenderSettings.skybox = daySkybox;
                    currentSkybox = daySkybox;
                    currentSkybox.SetFloat("_Rotation", currentRotation);
                }
            }

            // Calculate target intensity based on xRotation
            float targetIntensity = (xRotation >= -160f && xRotation <= 20f) ? nightIntensity : dayIntensity;

            // Smoothly transition to the target intensity
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * transitionSpeed);
            RenderSettings.skybox.SetFloat("_Exposure", currentIntensity);

            // Calculate target fog density based on xRotation
            float targetFogDensity = Mathf.Lerp(nightFogDensity, dayFogDensity, Mathf.InverseLerp(-160f, 20f, xRotation));

            // Smoothly transition to the target fog density
            currentFogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, Time.deltaTime * transitionSpeed);
            RenderSettings.fogDensity = currentFogDensity;

            // Calculate target color tint for daySkybox based on xRotation
            if (RenderSettings.skybox == daySkybox)
            {
                Color targetColor;
               

                    if (xRotation >= 0f && xRotation < 40f)
                    {
                        targetColor = Color.Lerp(morningColor, earlyMorningColor, Mathf.InverseLerp(0f, 40f, xRotation));
                    }
                    else if (xRotation >= 40f && xRotation < 110f)
                    {
                        targetColor = earlyMorningColor;
                    }
                    else if (xRotation >= 110f && xRotation < 120f)
                    {
                        targetColor = Color.Lerp(earlyMorningColor, eveningColor, Mathf.InverseLerp(110f, 120f, xRotation));
                    }
                    else if (xRotation >= 120f && xRotation < 180f)
                    {
                        targetColor = Color.Lerp(eveningColor, nightColor, Mathf.InverseLerp(120f, 180f, xRotation));
                    }
                    else if (xRotation >= 180f && xRotation < 330f)
                    {
                        targetColor = nightColor;
                    }
                    else if (xRotation >= -30f && xRotation < 0f)
                    {
                        targetColor = Color.Lerp(nightColor, morningColor, Mathf.InverseLerp(-30f, 0f, xRotation));
                    }
                    else if (xRotation >= -160f && xRotation < -30f)
                    {
                        targetColor = nightColor;
                    }
                    else
                    {
                        targetColor = morningColor;
                    }

                
                // Apply the target color to daySkybox
                daySkybox.SetColor("_Tint", targetColor);
            }

            // Wait for a fraction of a second before updating again
            yield return new WaitForSeconds(0.1f);
        }
    
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