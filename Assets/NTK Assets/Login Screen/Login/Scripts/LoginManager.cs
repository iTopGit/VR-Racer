using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // Data For Send
    private string email, password;
    private string apiUrl;
    private string apiKey ;

    // Json Object for Recieve
    public class JsonLoginReturn
    {
        public string message;
        public string username;
    }
    public string username;

    // TMP for Display
    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        string jsonText = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/config.json");
        ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonText);
        apiUrl = configData.lgn_url;
        apiKey = configData.lgn_key;
    }

    public void setData(string inputEmail, string inputPassword) 
    {
        email = inputEmail;
        password = inputPassword;
    }


    public async void startSendJsonToServer()
    {
        await SendLoginRequest(email, password);
    }

    async Task<IEnumerator> SendLoginRequest(string email, string password)
    {
        Debug.Log("Url: " + apiUrl + "\n key" + apiKey);
        // Send Data
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
        request.Headers.Add("api_key", apiKey);
        request.Headers.Add("Cookie", "Cookie_3=value");
        var content = new StringContent("{\r\n    \"email\": \"" + email + "\",\r\n    \"password\": \"" + password + "\"\r\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        // Get data
        var returnData = await response.Content.ReadAsStringAsync();
        Debug.Log(returnData);

        // json to object
        JsonLoginReturn loginReturn = JsonUtility.FromJson<JsonLoginReturn>(returnData);

        if (loginReturn.username != null )
        {
            username = loginReturn.username;
            UserContainer.username = username;
            Debug.Log("Username : " + username);
        }

        Debug.Log(loginReturn.message);

        if (loginReturn.message == "successfully")
        {
            UserContainer.email = email;
            Debug.Log("Scene 1: " + UserContainer.email);

            // go to title screen
            SceneManager.LoadScene(1);
        }
        else if (loginReturn.message == "Please check your email.")
        {
            // email not found
            textMeshPro.text = "Email not found!";
        }
        else if (loginReturn.message == "Please check your password.") 
        {
            // Password incorrect
            textMeshPro.text = "Password incorrect!";
        }
            return null;
    }
}
