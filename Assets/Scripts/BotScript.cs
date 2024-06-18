using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    [SerializeField] private CreateMedicineScript createMedicineScript;
    [SerializeField] private GameObject _sendingPanel, _botAvailabilityPanel, portal,z;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private TextMeshProUGUI _availabilityText;
    private GameObject Bot;
    private AudioSource sendBotSound;
    private bool _isBotAvailable=true;
    private bool _isBotCharging =false;
    private bool _isPanelUp=false;
    private int _medicineAmount;
    public int _deliveryTime = 30;
    public float _malariaReductionAmount = 0.1f;
    private string  _medicineAmountText;

    bool interactable = false;

    void Start(){
        Bot = GameObject.FindGameObjectWithTag("Bot");
        sendBotSound= GameObject.Find("sendBotSound").GetComponent<AudioSource>();
        //successSound= GameObject.Find("successSound").GetComponent<AudioSource>();
        _medicineAmount = GameObject.FindGameObjectWithTag("createMedicineDevice").GetComponent<CreateMedicineScript>()._medicineAmount;
         _medicineAmountText=GameObject.FindGameObjectWithTag("createMedicineDevice").GetComponent<CreateMedicineScript>()._medicineAmountText.text;
    }

     private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            
            if (_isBotCharging)
            {
                _isPanelUp = true;
                _botAvailabilityPanel.SetActive(true);
                _availabilityText.text = "Bot Is Charging";
            }
            else {
                interactable = true;
            }
        }
        
    }
    void Update(){
        _medicineAmountText="Medicine Amount = " + _medicineAmount;
        if (Input.GetKeyDown(KeyCode.Z) && interactable)
        {
            GetMeds();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_isPanelUp){
               CloseAvailabilityPanel();
            }
                
        }
    }
    private void GetMeds()
    {
       /* foreach (var item in createMedicineScript.ResultList)
        {
            if(item.sprite == createMedicineScript.medicineSprite && item.color.a == 1f)
            {
                _medicineAmount++;
            }
        }
        if (_medicineAmount >= 1)
        {*/
                    _sendingPanel.SetActive(true);
                    Invoke("SendBot", 2);
                    Invoke("ReduceMalaria", 10);
                    Invoke("BotCharging", 82);
                    Invoke("BotBack", _deliveryTime);
        z.SetActive(false);


               
       /* else
        {
            _isPanelUp = true;
            _botAvailabilityPanel.SetActive(true);
            _availabilityText.text = "BotAvailable";
            Invoke("CloseAvailabilityPanel", 1);
        }*/

        //}
    }

    private void SendBot(){
        _isBotAvailable=false;
        _sendingPanel.SetActive(false);
        Bot.GetComponent<Animator>().SetTrigger("bot");
        sendBotSound.Play();
        portal.SetActive(true);
        foreach (var item in createMedicineScript.ResultList)
        {
            if (item.sprite == createMedicineScript.medicineSprite && item.color.a == 1f)
            {
                GameObject _parent = item.transform.parent.gameObject;
                _parent.SetActive(false);
            }
        }
    }

    private void ReduceMalaria(){
        
        createMedicineScript.successSound.Play();
        _medicineAmount *=_medicineAmount;
       clip.frameRate += _malariaReductionAmount;
        _medicineAmount--;
    }
    void BotBack(){
        z.SetActive(true);
        _isBotAvailable = true;
        _isBotCharging= false;
    }
    void BotCharging(){
        portal.SetActive(false);
        _isBotCharging=true;
        _botAvailabilityPanel.SetActive(false);
    }
    void CloseAvailabilityPanel(){
         _botAvailabilityPanel.SetActive(false);
        _isPanelUp=false;
        interactable=false;
    }
}
