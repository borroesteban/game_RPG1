using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //change position to target position at a speed * time.deltatime
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //make the Y axis arrow face/point at target
        transform.up = target.transform.position - transform.position;
    }
}
