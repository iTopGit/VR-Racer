using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupButton : MonoBehaviour
{
    public string webURL = "http://172.104.189.115:56733/signup";
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LinkToWeb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LinkToWeb()
    {
        Application.OpenURL(webURL);
    }
}
