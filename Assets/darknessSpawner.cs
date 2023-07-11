using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class darknessSpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public int spawnSize;

    void OnTriggerEnter2D(Collider2D other)
        {   
            if (other.name == "Player")
            {
                for (int i = 0; i < spawnSize; i=i+1)
                {
                  Instantiate(enemyToSpawn, gameObject.transform.position, gameObject.transform.rotation);  
                  
                }
                
            }
        }
}
