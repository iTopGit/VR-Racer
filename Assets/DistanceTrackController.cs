using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrackController : MonoBehaviour
{
    // Brake Count.
    int brakeAmount = 0;
    // Crosswalk violated.

    // Car crashed.
    int crash_count = 0;

    // Player speed receiving.
    CarController carController;

    // Map's Sector Partitioning.
    private int currSector = 1;

    // for CSV writing.
    private CSVWriter csvWriter;

    // Array for store score data.
    // currently only second played and distance passed.
    List<string[]> dataToExport = new List<string[]>();

    // check how player have played game.
    int game_lap = 1;
    int tracker_amount = 8;
    int distance_amount = 7;

    // actual amount of tracker is 8.
    public GameObject[] tracker = new GameObject[8];
    // actual distance each tracker is 7.
    int[] distance_each_tracker = new int[7];

    public int DistancePerLap = 0;
    bool tracking = false;
    public GameObject playerMesh;
    public int currTracker = 1;
    int passedSector = 0;
    
    int currDistance = 0;
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // player's CarController intiating
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        
        // .csv writer initiating
        csvWriter = gameObject.GetComponent<CSVWriter>();

        // init first role as table's column
        dataToExport.Add(new string[] { 
            "Time(seconds)", 
            "Distance(percent)", 
            "Sector",
            "Speed(Km/hr)",
            "Crash count"});

        if (tracker[0] != null)
        {
            initDistance();
        }
    }

    void initDistance()
    {
        Vector3 temp1, temp2;
        for(int i=0; i<distance_amount; i++)
        {
            temp1 = tracker[i].transform.position;
            temp2 = tracker[i+1].transform.position;
            // this is the line where we find distance between each 2 tracker.
            distance_each_tracker[i] = (int)(temp2 - temp1).magnitude;
            // Debug.Log(distance_each_tracker[i]);
            DistancePerLap += distance_each_tracker[i];
        }
        Debug.Log("Distance per Lap is " + DistancePerLap);
        Debug.Log("Distance of two Lap is " + (DistancePerLap * 2));

    }


    IEnumerator performTracking()
    {
        // tracking will continue until player've played 2 lap or out of time.
        while (game_lap <= 2 && timer <= 60 * 8) {
            /*
            to display distance from start, formula should be
            current_progression =   distanceEachTracker[curr_tracker-1] -
                                    (player to next tracker length) +
                                    passed_distance
             */
            if (currDistance <= DistancePerLap * 2) {
                currDistance = (int)
                    (distance_each_tracker[currTracker - 1] -
                    (playerMesh.transform.position -
                        tracker[currTracker].transform.position).magnitude +
                    passedSector);
            } else {
                currDistance = DistancePerLap * 2;
            }
            // Debug.Log(timer + " " + currDistance);
            // add player's data to List
            addData(
                timer,
                (100 * currDistance / (DistancePerLap * 2)),
                currSector, 
                (int)carController.currentSpeed,
                crash_count);

            yield return new WaitForSeconds(1.0f);
            timer++;
        }
        if(game_lap > 2)
        {
            game_lap = 2;
        }
        // add player's data to List
        addData(
            timer, 
            100, 
            game_lap = 2, 
            (int)carController.currentSpeed,
            crash_count);

        // After Game end, start writing .csv file.
        csvWriter.WriteToCSV(dataToExport);
    }
    public void enableTracking()
    {
        tracking = true;
        if(playerMesh != null) { 
            StartCoroutine(performTracking());
        }
    }

    void addData(
        int seconds, 
        int distance, 
        int lap, 
        int currentSpeed, 
        int crash_count
        ) {
        dataToExport.Add(new string[] { 
            seconds.ToString(), 
            distance.ToString() + "%", 
            lap.ToString(), 
            currentSpeed.ToString(),
            crash_count.ToString()
        });
    }
    public void moveTracker()
    {
        passedSector += distance_each_tracker[currTracker - 1];
        currTracker = (currTracker+1) % 8;
        // this statement check if player've pass 1 round.
        if(currTracker == 0) { 
            currTracker++;
            game_lap++;
        }
        // Debug.Log("current tracker: " + currTracker + ", and current lap is: " + game_lap);
    }

    public void changeSector(int index)
    {
        currSector = index;
        Debug.Log("Sector " + currSector);
    }

    public void crashCounting()
    {
        crash_count++;
    }

    public void brakeCheck()
    {
        brakeAmount++;
    }
}
