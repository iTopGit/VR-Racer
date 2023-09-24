using UnityEngine;
using TMPro;

public class CopyUIText : MonoBehaviour
{
    public TextMeshProUGUI sourceText; // Reference to the source TextMeshProUGUI component
    public TextMeshProUGUI targetText; // Reference to the target TextMeshProUGUI component

    // This function is called when a button or event triggers the text copy
    void Update()
    {
        if (sourceText != null && targetText != null)
        {
            // Copy the text from the source TextMeshProUGUI component to the target TextMeshProUGUI component in real-time
            targetText.text = sourceText.text;
        }
        else
        {
            Debug.LogError("SourceText or TargetText is not assigned!");
        }
    }
}
