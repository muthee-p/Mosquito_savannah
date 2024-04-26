using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class TakeSample : MonoBehaviour
{
    [SerializeField] private GameObject _takeSample;
    [SerializeField] Image _currentTask;
    [SerializeField] private Sprite _fullTestTube;
    public int _sampleAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _takeSample.SetActive(true);
            _currentTask.sprite = _fullTestTube;
            Invoke("HidePanel", 1.5f);
        }
    }
    private void HidePanel(){
        _sampleAmount++;
        _takeSample.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled=false;
    }
}
