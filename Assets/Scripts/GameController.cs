using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject inventoryPanel;

    void Start(){
        Cursor.visible=false;
    }
     public void OpenInventoryPanel(){
        inventoryPanel.SetActive(true);
    }
    public void CloseInventoryPanel(){
        inventoryPanel.SetActive(false);
        Invoke("HideCursor", 1);
    }
    void HideCursor(){
        Cursor.visible = false;
    }
}