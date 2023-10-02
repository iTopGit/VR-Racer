using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    private Button button;
    public InputFieldManager emailField, passwordField;
    private string email, password;

    private LoginManager LM;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(selectButton);

        LM = GameObject.Find("Login Manager").GetComponent<LoginManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void selectButton()
    {
        string buttonName = button.gameObject.name;

        if (buttonName == "Login Button")
        {
            email = emailField.getData();
            password = passwordField.getData();
            LM.setData(email, password);
            LM.startSendJsonToServer();
        }
        else if (buttonName == "Exit Button")
        {
            Application.Quit();
        }
    }
}
