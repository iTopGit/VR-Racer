using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(selectButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectButton()
    {
        Application.Quit();
    }
}
