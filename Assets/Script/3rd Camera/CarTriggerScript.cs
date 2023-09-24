using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CarTriggerScript : MonoBehaviour
{
    public TextMeshProUGUI uiText; // Reference to the UI Text element
    public List<GameObject> allowedTriggerObjects; // List of allowed trigger objects

    private string originalText; // Stores the original text

    void Start()
    {
        // Get the TextMeshProUGUI component from the game object
        uiText = GetComponent<TextMeshProUGUI>();

        // Store the original text
        originalText = uiText.text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAllowedTriggerObject(other.gameObject))
        {
            uiText.text = other.gameObject.name;
        }
    }

    private bool IsAllowedTriggerObject(GameObject obj)
    {
        // Check if the triggering object is in the list of allowed trigger objects
        return allowedTriggerObjects.Contains(obj);
    }
}
