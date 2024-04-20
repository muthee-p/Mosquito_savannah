using UnityEngine;

public class TakeSample : MonoBehaviour
{
    [SerializeField] private GameObject _takeSample;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _takeSample.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _takeSample.SetActive(false);
        }
    }
}
