using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System.Net.Http.Headers;
using RockVR.Video;

// currently though if Sector value goes wrong if player drive backward
public class DistanceTrackController : MonoBehaviour {
    // For managing GUI.
    [SerializeField] GameObject labText;
    [SerializeField] GameObject sectorText;

    // Speed limit zone indicator
    int speedZone;

    // Scenemanager
    GameManager gameManager;
    // passed roundabout and crosswalk checking.
    int crosswalkPassed = 0;
    int entry_crosswalk = 0;
    int currentRoundabout = 0;
    int entry_Roundabout = 0;
    int currRoundaboutState = 0;

    // Brake Count.
    int brakeAmount = 0;

    // map crashed.
    int map_crashed = 0;
    // Car crashed.
    int crash_count = 0;
    // Player Overturn count.
    int overturnedTimes = 0;

    // Player speed receiving.
    CarController carController;
    // for CSV writing.
    private CSVWriter csvWriter;
    // Array for store score data.
    // currently only second played and distance passed.
    List<string[]> dataToExport = new List<string[]>();

    // Map's Sector Partitioning.
    private int currSector = 1;
    private int[] sectorLimit = { 40, 40, 60 };

    // check how player have played game.
    int game_lap = 1;
    int tracker_amount = 8;
    int distance_amount = 7;
    int timeLimit = 60 * 8; // 8 minute to second.

    // actual amount of tracker is 8.
    public GameObject[] tracker = new GameObject[8];
    // actual distance each tracker is 7.
    int[] distance_each_tracker = new int[7];

    public int DistancePerLap = 0;
    public GameObject playerMesh;
    public int currTracker = 1;
    int passedSector = 0;

    int currDistance = 0;
    int timer = 0;

    // Start is called before the first frame update
    void Start() {
        updateGameUI();

        speedZone = 0;

        // player's CarController intiating
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();

        // .csv writer initiating
        csvWriter = gameObject.GetComponent<CSVWriter>();

        // init first role as table's column
        dataToExport.Add(new string[] {
            "Time",                 // seconds
            "Distance",             // percent 
            "Speed",                // Km/hr
            "Lap",
            "Sector",

            "Brake Times",          // check whole brake time.
            "Map Crashed",          // check if player is crashing with game's map or fountain.
            "Car Crashed",
            "Passed Circus",        // If player didn't stop moving at Circus area, We will count them as pass.
            "Circus entry",         // speed before entry circus
            
            "Passed Crosswalk",     // If player stop at right distance between Crosswalk, We will count them as pass.
            "Crosswalk entry",     // speed before entry crosswalk  
            "Car Overturned",
            "Speed Zone"
        });

        if (tracker[0] != null) { initDistance(); }
    }

    private void Update()
    {
        if(Input.GetKeyDown("l")) {
            // end game.
            sendingData();
            SceneManager.LoadScene(3);
        } else if (Input.GetKeyDown("p")) {
            VideoCaptureCtrl.instance.StopCapture();
            SceneManager.LoadScene(1);
        }

    }
    void initDistance()
    {
        Vector3 temp1, temp2;
        for (int i = 0; i < distance_amount; i++)
        {
            temp1 = tracker[i].transform.position;
            temp2 = tracker[i + 1].transform.position;
            // this is the line where we find distance between each 2 tracker.
            distance_each_tracker[i] = (int)(temp2 - temp1).magnitude;
            DistancePerLap += distance_each_tracker[i];
        }

    }

    IEnumerator performTracking()
    {
        int percentageDistance;
        // tracking will continue until player've played 2 lap or out of time.
        while (game_lap <= 2 && timer <= timeLimit)
        {
            // if current distance wasn't out of goal.
            if (currDistance <= DistancePerLap * 2)
            {
                currDistance = (int)
                    (distance_each_tracker[currTracker - 1] -
                    (playerMesh.transform.position -
                        tracker[currTracker].transform.position).magnitude +
                    passedSector);
            }
            else
            {
                currDistance = DistancePerLap * 2;
            }

            if (passedSector < 0) { passedSector++; }
            // Debug.Log(timer + " seconds, " + currDistance + " value, " + game_lap + " lap.");
            // add player's data to List
            percentageDistance = (100 * currDistance / (DistancePerLap * 2));
            addData(
                timer,
                percentageDistance,
                (int)carController.currentSpeed,
                brakeAmount,
                map_crashed,
                overturnedTimes
                );
            yield return new WaitForSeconds(1.0f);
            timer++;
            // Debug.Log("Time: " + timer + " seconds. distance: " + percentageDistance + "%");

        }
        // Debug.Log("Loop Tracking Ended.");
        if (game_lap > 2) { game_lap = 2; }
        // for player ends game without reach 2 lap(already played for 8 minutes).
        percentageDistance = (100 * currDistance / (DistancePerLap * 2));
        if (percentageDistance >= 93) { percentageDistance = 100; }

        // add player's data to List
        addData(
            timer,
            percentageDistance,
            (int)carController.currentSpeed,
            brakeAmount,
            map_crashed,
            overturnedTimes
            );
        // After Game end, start writing .csv file.
        csvWriter.WriteToCSV(dataToExport);

        // end game.
        sendingData();
        SceneManager.LoadScene(3);

    }

    public void enableTracking() { if (playerMesh != null) { StartCoroutine(performTracking()); } }

    void sendingData() {
        SendCSV sendCSV = gameObject.GetComponent<SendCSV>();
        SendVID sendVID = gameObject.GetComponent<SendVID>();

        if(UserContainer.email == null) { UserContainer.email = "vokox44702@utwoko.com"; }

        string directoryPath = Application.streamingAssetsPath;
        
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo[] files = directoryInfo.GetFiles("*.mp4");

        List<string> mp4FileList = new List<string>();

        foreach (FileInfo file in files) { mp4FileList.Add(file.Name); }

        // mp4Files will get files name without path.
        string[] mp4Files = mp4FileList.ToArray();

        if (mp4Files.Length == 2) {
            Debug.Log("File is ready to be sent.");
            //UploadFiles(mp4Files[0], mp4Files[1]);
            UserContainer.video1 = mp4Files[0];
            UserContainer.video2 = mp4Files[1];
        }
    }

    void updateGameUI() {
        labText.GetComponent<TextMeshProUGUI>().text = "LAB " + game_lap;
        sectorText.GetComponent<TextMeshProUGUI>().text = "SECTOR " + currSector;
    }

    void addData(
        int seconds,
        int distance,
        int currentSpeed,
        int brake_count,
        int mapCrash,
        int overturned_times)
    {

        // Row at Roundabout and Crosswalk in the new lap, They should not be duplicated.
        if (game_lap == 2) {
            if (0 < currentRoundabout && currentRoundabout < 6) { currentRoundabout += 5; }
            if (0 < entry_Roundabout && entry_Roundabout < 6) { entry_Roundabout += 5; }
            if (0 < crosswalkPassed && crosswalkPassed < 4) { crosswalkPassed += 3; }
            if (0 < entry_crosswalk && entry_crosswalk < 4) { entry_crosswalk += 3; }
        }

        // Row at the finished game, Player shouldn't stay in Roundabout or Crosswalk
        if (distance == 100) {
            currentRoundabout = 0;
            crosswalkPassed = 0;
            game_lap = 2;
            currSector = 3;
        }

        dataToExport.Add(new string[] {
            seconds.ToString(),
            distance.ToString(),
            currentSpeed.ToString(),
            game_lap.ToString(),
            currSector.ToString(),

            brake_count.ToString(),
            mapCrash.ToString(),
            crash_count.ToString(),
            currentRoundabout.ToString(),
            entry_Roundabout.ToString(),

            crosswalkPassed.ToString(),
            entry_crosswalk.ToString(),
            overturned_times.ToString(),
            speedZone.ToString()
        });
    }
    public void moveTracker()
    {
        passedSector += distance_each_tracker[currTracker - 1];
        currTracker = (currTracker + 1) % 8;
        // this statement check if player've pass 1 round.
        if (currTracker == 0)
        {
            currTracker++;
            game_lap++;
        }
        //Debug.Log("current tracker: " + currTracker + ", and current lap is: " + game_lap + "\npassed sector: " + currSector);
        updateGameUI();
    }

    public void changeSector(int index)
    {
        currSector = index;
        updateGameUI();
    }

    public void crashCounting() { crash_count++; }

    public void brakeChecking() { brakeAmount++; }

    public void MapCrashed() { map_crashed++; }

    public void trafficCirclePassing(int id) {
        if (0 < id && game_lap == 1) {
            currentRoundabout = id;
        } else if (0 < id && game_lap == 2) {
            currentRoundabout = id + 5;
        } else {
            currentRoundabout = 0;
        }
    }

    public void entry_trafficCircle(int id)
    {
        if (0 < id && game_lap == 1) {
            entry_Roundabout = id;
        } else if (0 < id && id + 5 < 11 && game_lap == 2) {
            entry_Roundabout = id + 5;
        } else {
            entry_Roundabout = 0;
        }
    }

    public void CrosswalkPassing(int id)
    {
        if (0 < id && game_lap == 1) {
            crosswalkPassed = id;
        } else if (0 < id && game_lap == 2) {
            crosswalkPassed = id + 3;
        } else {
            crosswalkPassed = 0;
        }
    }

    public void entry_Crosswalk(int id)
    {
        if (0 < id && game_lap == 1) {
            entry_crosswalk = id;
        } else if (0 < id && game_lap == 2) {
            entry_crosswalk = id + 3;
        } else {
            entry_crosswalk = 0;
        }
    }

    public void overturnedChecking() { overturnedTimes++; }

    public void setSpeedZone(int i) {
        speedZone = i;
    }
}
