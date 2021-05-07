using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SpawnBullet : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject spawnBullet;

    private int nextBulletSpawnTime = 2;
    private bool hasBulletSpawned = false;

    int numberShots = 0;

    // Update is called once per frame
    void Update()
    {
        fireBullet();

        // Limit the number of bullets that can be spawned
        if(Time.time >= nextBulletSpawnTime)
        {
            nextBulletSpawnTime = Mathf.FloorToInt(Time.time + 1);
            hasBulletSpawned = false;
        }
    }

    private void fireBullet()
    {
        // Limit bullet spawn rate
        if(hasBulletSpawned) { return; }

        // Check that button is pressed
        float fire = Input.GetAxis("Fire1");

        if(fire > 0)
        {
            // Increment shot counter
            numberShots += 1;

            // Update pertinent shots file
            updateShotsRecord();

            // Instantiate a bullet at the spawn position, taking the camera's rotation in mind
            var bullet = Instantiate(spawnBullet, bulletSpawnPoint.transform.position, Camera.main.transform.rotation) as GameObject;

            // Make it as fast as possible (max velocity for the trigger to work)
            bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 345;

            // Destroy after 2 seconds
            Destroy(bullet, 2);
            
            // Limit number of bullets that can be spawned
            hasBulletSpawned = true;
        }
    }

    private void updateShotsRecord()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName != "00Tutorial")
        {
            string writePath = sceneName + "Accuracy.txt";
            StreamWriter shotRecord = new StreamWriter(writePath);
            float shotAccuracy = 10.0f/numberShots;
            shotRecord.WriteLine(sceneName + " Accuracy: " + shotAccuracy);
            shotRecord.Close();
        }
    }
}