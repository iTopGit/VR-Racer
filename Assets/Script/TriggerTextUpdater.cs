using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI uiText; // Reference to the UI Text element.
    private bool isTriggered = false;

    // This function is called when the object is triggered.
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player")) // Adjust the tag to match the object that can trigger this.
        {
            isTriggered = true;
            UpdateUIText();
        }
    }

    // Update the UI Text element.
    private void UpdateUIText()
    {
        if (uiText != null)
        {
            uiText.text = gameObject.name;
        }
    }
}
