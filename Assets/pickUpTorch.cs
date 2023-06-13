using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class pickUpTorch : MonoBehaviour
{
//access the torch
//access the torchs 2dcollider
//make an oncollide function that sets the torch as collider children
//destroy torch after 2 minutes
//object1. transform. parent = object2. transform.

/* 
private Transform torchTransformComponent;
private BoxCollider2D torchColliderComponent;
private Transform playerTransform;

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
*/

private Transform torchTransformComponent;
private Transform playerTransform;
private Transform playerTorchHolder;
private BoxCollider2D torchColliderComponent;
private bool picked;
public float torchDuration;
private float lifeTime;
private float counterClock;
private float lightValue;
private float value;
private Transform torchLight;
private Light2D torchComponent;
private Transform flickerTorch;
private Light2D torch;



    // Update is called once per frame
   
    //pick up torch on contact
    private void OnTriggerEnter2D(Collider2D other)
    {   

        torchTransformComponent = GetComponent<Transform>();        
        torchColliderComponent = GetComponent<BoxCollider2D>();
        playerTorchHolder = GameObject.Find("Player/torchHolder").transform;
        if (other.tag == "torchGrabber")
        {
            torchTransformComponent.parent = playerTorchHolder;
            torchTransformComponent.position = playerTorchHolder.transform.position;
            picked = true;
           
        }
    }

    void Update()
    {
         torchDeathOverTime();
    }

    private void torchDeathOverTime()
    {
        if (picked == true)
        {
            lifeTime = torchDuration;
            flickerTorch = playerTorchHolder.transform.GetChild(0);
            torchLight = flickerTorch.GetChild(0);
            torch = torchLight.GetComponent<Light2D>();
            counterClock += Time.deltaTime / torchDuration;
            lightValue = Mathf.Lerp(0.29f, 0.01f, counterClock);
            torch.intensity = lightValue;
            Destroy(gameObject, lifeTime);
        }
    }
    
}

