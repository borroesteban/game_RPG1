using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darknessSpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    // Update is called once per frame

void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.name == "Player")
        {
            Instantiate(enemyToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }


}
