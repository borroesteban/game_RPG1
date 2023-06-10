using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpTorch : MonoBehaviour
{
//access the torch
//access the torchs 2dcollider
//make an oncollide function that sets the torch as collider children
//destroy torch after 2 minutes
//object1. transform. parent = object2. transform.
private Transform torchTransformComponent;
private BoxCollider2D torchColliderComponent;


    // Update is called once per frame
    void Update()
    {
        torchTransformComponent = GetComponent<Transform>();        
        torchColliderComponent = GetComponent<BoxCollider2D>();
        
    }
    //pick up torch on contact
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fighter")
        {
            torchTransformComponent.parent = other.transform;
            torchTransformComponent.position = other.transform.position;
        }

    }
}
