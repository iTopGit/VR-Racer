using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendCSV : MonoBehaviour
{
    private string apiUrl;
    private string apiKey;

    private string email;

    // mocked up value
    private string name_state = "roundabout_test_simple";
    private string level= "simple";
    private string car_roundabout = "10";
    private string car_inroad = "0";
    private string time = "day";
    private string weather = "no";
    private string lane_check = "0";


    public class JsonScoreReturn
    {
        public string message;
        public int score;
        public float score_compare;
        public float speed;
        public float speed_compare;
    }

    // Start is called before the first frame update
    void Start() {

        if (UserContainer.email == null) { UserContainer.email = "vokox44702@utwoko.com"; Debug.Log("set email"); }
    }

    public async Task<IEnumerator> SendCSVToServer(string video1, string video2)
    {
        string jsonText = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/config.json");
        ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonText);
        apiUrl = configData.csv_url;
        apiKey = configData.csv_key;
        email = UserContainer.email;

        Debug.Log("check path of video:\n" + video1 + "\n" + video2);
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
        request.Headers.Add("api_key", apiKey);
        request.Headers.Add("Cookie", "Cookie_3=value");
        var content = new MultipartFormDataContent();
        /*
        string stringJson = $"{{\"email\": \"{email}\", \"video1\": \"{video1}\", \"video2\": \"{video2}\"" +
            $", \"name_state\": \"{name_state}\"" +
            $", \"level\": \"{level}\"" +
            $", \"car_roundabout\": \"{car_roundabout}\"" +
            $", \"car_inroad\": \"{car_inroad}\"" +
            $", \"time\": \"{time}\"" +
            $", \"weather\": \"{weather}\"" +
            $", \"lane_check\": \"{lane_check}\"" +
            $"}}";
         */
        string stringJson = $"{{\"email\": \"{email}\", \"video1\": \"{video1}\", \"video2\": \"{video2}\"" +
            $", \"name_state\": \"{name_state}\"" +
            $"}}";

        var json = new StringContent(stringJson, System.Text.Encoding.UTF8, "application/json");
        content.Add(json, "json", "jsonFile.json");

        // ADD PATH HERE
        content.Add(new StreamContent(File.OpenRead(Application.streamingAssetsPath + "/data.csv")), "csv", "data.csv");
        request.Content = content;

        Debug.Log("sending Data.");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Debug.Log("Data has been sent.");

        // Get data
        var returnData = await response.Content.ReadAsStringAsync();

        // json to object
        JsonScoreReturn scoreReturn = JsonUtility.FromJson<JsonScoreReturn>(returnData);

        Debug.Log(scoreReturn.message);
        UserContainer.score = scoreReturn.score;
        UserContainer.score_compare = scoreReturn.score_compare;
        UserContainer.speed = scoreReturn.speed;
        UserContainer.speed_compare = scoreReturn.speed_compare;
        if (scoreReturn.message != "Data successfully processed")
        {
            Debug.Log("Data haven't been sent.");
        }

        gameObject.GetComponent<EndingSceneManager>().sendVDO();
        
        return null;
    }

    
}
