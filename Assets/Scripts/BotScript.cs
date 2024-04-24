using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    [SerializeField] private GameObject _sendingPanel, _botAvailabilityPanel;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private TextMeshProUGUI _availabilityText;
    private GameObject Bot;
    private bool _isBotAvailable=true;
    private bool _isBotCharging =false;
    private bool _isPanelUp=false;
    public int _deliveryTime = 30;
    public float _malariaReductionAmount = 0.1f;

    void Start(){
        Bot = GameObject.FindGameObjectWithTag("Bot");
    }

     private void OnTriggerEnter(Collider other)
    {
        if(_isBotAvailable){
            if (other.CompareTag("Player"))
            {
                _sendingPanel.SetActive(true);
                Invoke("SendBot", 2);
                Invoke("ReduceMalaria", 10);
                Invoke("BotCharging", 22);
                Invoke("BotBack", _deliveryTime);
            }
            else if(_isBotAvailable &&_isBotCharging){
                _isPanelUp=true;
                _botAvailabilityPanel.SetActive(true);
                _availabilityText.text = "Bot Is Charging";
            } 
            else{
                _isPanelUp=true;
                _botAvailabilityPanel.SetActive(true);
                _availabilityText.text="BotAvailable";
                Invoke("CloseAvailabilityPanel",1);
            }
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

    private void SendBot(){
        _isBotAvailable=false;
        _sendingPanel.SetActive(false);
        Bot.GetComponent<Animator>().SetTrigger("bot");
    }

    private void ReduceMalaria(){
       clip.frameRate += _malariaReductionAmount;
    }
    void BotBack(){
        _isBotAvailable = true;
        _isBotCharging= false;
        // Bot.GetComponent<Animator>().enabled = false;
    }
    void BotCharging(){
        _isBotCharging=true;
        _botAvailabilityPanel.SetActive(false);
    }
    void CloseAvailabilityPanel(){
         _botAvailabilityPanel.SetActive(false);
        _isPanelUp=false;
    }
}
