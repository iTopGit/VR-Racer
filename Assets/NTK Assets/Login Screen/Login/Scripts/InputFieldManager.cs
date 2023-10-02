using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldManager : MonoBehaviour
{
    private TMP_InputField inputData;
    private string data;
    // Start is called before the first frame update
    void Start()
    {
        inputData = GetComponent<TMP_InputField>();
        inputData.onEndEdit.AddListener(getInputText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getInputText(string inputData)
    {
        data = inputData;
    }

    public string getData() 
    { 
        return data; 
    }
}
