 using UnityEngine;

public class UnlockCusor: MonoBehaviour {
   
    void Start(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}