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
private Transform playerTransform;

    // Update is called once per frame
   
    //pick up torch on contact
    private void OnTriggerEnter2D(Collider2D other)
    {   
        playerTransform = GameManager.instance.player.transform;
        torchTransformComponent = GetComponent<Transform>();        
        torchColliderComponent = GetComponent<BoxCollider2D>();
        if (other.tag == "Fighter")
        {
            torchTransformComponent.parent = playerTransform;
            torchTransformComponent.position = playerTransform.position + new Vector3(0.06f,0.06f,0);
            
        }

    }
}
