using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TargetScript : MonoBehaviour
{
    private int remainingTargets = 10;

    private float startTime;
    private float finishTime;

    private float[] times = new float[10];
    private int timesIndex = 0;

    private void Start()
    {
        startTime = Time.time;
    }
    
    private void Update() {
        if(remainingTargets == 0)
        {
            recordAverageTime();

            // Quit on last scene
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "05CDGainInkwell")
            {
                Application.Quit();
            }

            // Or cycle to next
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            // Calculate completion time and prepare for next target
            finishTime = Time.time;
            float completionTime = finishTime - startTime;
            startTime = Time.time;

            // Add completion time to list
            times[timesIndex] = completionTime;
            timesIndex++;

            // Destroy Bullet
            Destroy(col.gameObject);

            // Generate random valid (X, Y, Z) coordinates
            float randomX = Random.Range(-25, 25);
            float randomY = Random.Range(10, 50);
            float randomZ = Random.Range(-29, 29);

            // Teleport to random location if there are still targets left to hit
            this.transform.position = new Vector3(randomX, randomY, randomZ);
            
            remainingTargets -= 1;
        }
    }

    private float calculateAverageTime()
    {
        float result = 0.0f;
        for(int i = 0; i < times.Length; i++)
        {
            result += times[i];
        }
        result /= times.Length;
        return result;
    }

    private void recordAverageTime()
    {
        float averageTime = calculateAverageTime();

        string sceneName = SceneManager.GetActiveScene().name;
        string writePath = sceneName + "AvgTime.txt";

        StreamWriter timeRecord = new StreamWriter(writePath);
        timeRecord.WriteLine(sceneName + " Average Time: " + averageTime);
        timeRecord.Close();
    }
}
