using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject _labCollectablePanel;

     public void CloseCollectablePanel(){
        Debug.Log
        ("Pressed");
        _labCollectablePanel.SetActive(false);
    }
}