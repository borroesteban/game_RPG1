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
private Transform playerTorchHolder;
private Transform torchLight;
private Transform flickerTorch;



private Light2D torch;
private BoxCollider2D torchColliderComponent;
private bool picked;
public float torchDuration;
private float lifeTime;
private float counterClock;
private float lightValue;

//stuff used for throwing function
private GameObject itemHolding;
public GameObject destroyEffect;
public Vector3 Direction { get; set; }
private Vector3 target;
Camera cam;



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
            itemHolding = torchTransformComponent.gameObject;
            
            picked = true;       
        }
    }
    public void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        torchDeathOverTime();





        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (itemHolding)
            {
                StartCoroutine(ThrowItem(itemHolding));
            }
        }

        IEnumerator ThrowItem(GameObject itemHolding)
        {
            getMousePosition();
            Vector3 startPoint = itemHolding.transform.position;
            Vector3 endPoint = target;
            itemHolding.transform.parent = null;
            for (int i = 0; i < 25; i++)
            {
                itemHolding.transform.position = Vector3.Lerp(startPoint, endPoint, i * .04f);
                yield return null;
            }
            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = true;
            Instantiate(destroyEffect, itemHolding.transform.position, Quaternion.identity);
            Destroy(itemHolding);
        }
    }

    private void torchDeathOverTime()
    {
        if (picked == true)
        {
        lifeTime = torchDuration;
        try
        {
                if (playerTorchHolder.transform.GetChild(0))
                {
                flickerTorch = playerTorchHolder.transform.GetChild(0);
                torchLight = flickerTorch.GetChild(0);
                torch = torchLight.GetComponent<Light2D>();
                counterClock += Time.deltaTime / torchDuration;
                lightValue = Mathf.Lerp(0.29f, 0.01f, counterClock);
                torch.intensity = lightValue;
                Destroy(gameObject, lifeTime);
                }
        }
            catch
            {              
            }
        }
    }
        private void getMousePosition()
    {
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(cam.transform.position.z);
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;
            target = mouseWorldPosition;
    }

}

