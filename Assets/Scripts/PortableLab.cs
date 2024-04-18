using UnityEngine;

public class PortableLab : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}