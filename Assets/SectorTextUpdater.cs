using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SectorTextUpdater : MonoBehaviour {
    public TextMeshProUGUI uiText; // Reference to the UI Text element.

    // This function is called when the object is triggered.
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) // Adjust the tag to match the object that can trigger this.
        {
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
