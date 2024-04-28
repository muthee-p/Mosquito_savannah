using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; // Array to store all panels

    private GameObject activePanel; // Currently active panel

    private void Start()
    {
        // Deactivate all panels except the first one
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        activePanel = panels[0];
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        // Check if the panel to open is different from the currently active one
        if (panelToOpen != activePanel)
        {
            // Deactivate the currently active panel
            activePanel.SetActive(false);

            // Activate the new panel
            panelToOpen.SetActive(true);

            // Update the activePanel variable
            activePanel = panelToOpen;
        }
    }
}
