using UnityEngine;

public class WaterSourcePanelScript: MonoBehaviour {
  
    public void ActivateTakeSample(){
        GameObject.FindGameObjectWithTag("water_source_1").GetComponent<BoxCollider>().isTrigger=true;
    }
}