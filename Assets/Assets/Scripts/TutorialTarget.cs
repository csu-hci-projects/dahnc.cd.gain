using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTarget : MonoBehaviour
{
    public int remainingTargets;

    private void Update() {
        if(remainingTargets == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
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
}
