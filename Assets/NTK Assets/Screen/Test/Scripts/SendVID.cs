using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using System.Net.Http.Headers;

public class SendVID : MonoBehaviour
{
    private string apiUrl;
    private string apiKey;

    private string email;

    private string directoryPath;

    EndingSceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        directoryPath = Application.streamingAssetsPath;
        sceneManager = gameObject.GetComponent<EndingSceneManager>();

        if(UserContainer.email == null) { UserContainer.email = "vokox44702@utwoko.com"; Debug.Log("set email"); }
    }

    public async Task uploadVideo(string video1, string video2)
    {
        string jsonText = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/config.json");
        ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonText);
        apiUrl = configData.vid_url;
        apiKey = configData.vid_key;

        email = UserContainer.email;
        directoryPath = Application.streamingAssetsPath;

        string file1Path = directoryPath + "/" + video1;
        string file2Path = directoryPath + "/" + video2;

        Debug.Log("check path of video:\n" + video1 + "\n" + video2);

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
        var content = new MultipartFormDataContent();
        var contentType = new MediaTypeHeaderValue("video/mp4");

        // Add the first video file.
        var file1Stream = File.OpenRead(file1Path);
        var file1Content = new StreamContent(file1Stream);
        file1Content.Headers.ContentType = contentType;
        content.Add(file1Content, "video1", video1);

        // Add the second video file.
        var file2Stream = File.OpenRead(file2Path);
        var file2Content = new StreamContent(file2Stream);
        file2Content.Headers.ContentType = contentType;
        content.Add(file2Content, "video2", video2);
        
        // Set the request content.
        request.Content = content;

        client.Timeout = TimeSpan.FromSeconds(1260);

        request.Headers.Add("api_key", apiKey);

        Debug.Log("Send api");
        // Send the request asynchronously.
        var response = await client.SendAsync(request);

        // Check the response status code.
        response.EnsureSuccessStatusCode();

        try
        {
            // Get the response content as a string.
            var responseString = await response.Content.ReadAsStringAsync();

            // Print the response content to the console.
            Debug.Log(responseString);
            sceneManager.isLoaded(true);
        }
        catch (Exception e)
        {
            // An error occurred.
            Debug.LogError("Failed to upload videos: " + e.Message);
            sceneManager.isLoaded(false);
        }

    }
}
