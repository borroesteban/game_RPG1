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
//variables for torch dead coroutine

/* private float TDClightValue;
private float TDCcounterClock;
private Light2D TDCtorch;
private Transform TDCtorchLight;
public float TDCtorchDuration;*/

public float lifeTimeAfterThrown;
float counterClock2;
private Light2D torch2;
public int torchDuration2;

//stuff used for throwing function

private GameObject itemHolding;
public GameObject destroyEffect;
//public GameObject destroyEffectDebris;
public Vector3 Direction { get; set; }
private Vector3 target;
Camera cam;
public float rotateSpeed;
public float motionSpeed;
public float onDeathRotation;



    //pick up torch on contact
    private void OnTriggerEnter2D(Collider2D other)
    {   
        torchTransformComponent = GetComponent<Transform>();        
        torchColliderComponent = GetComponent<BoxCollider2D>();
        playerTorchHolder = GameObject.Find("Player/torchHolder").transform;
        if (other.tag == "torchGrabber" && picked==false)
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
                
                itemHolding=null;
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
                itemHolding.transform.position = Vector3.Lerp(startPoint, endPoint, i * motionSpeed);
                itemHolding.transform.Rotate(0,0,i*rotateSpeed);
                yield return null;
            }
            
            for (int i = 0; i < 25; i++)
            {
                itemHolding.transform.Rotate(0,0,i*rotateSpeed/onDeathRotation);
                yield return null;
            }

            for (int i = 0; i < torchDuration2; i++)
            {
                torch2 = itemHolding.transform.GetComponent<Light2D>();
                counterClock2 += Time.deltaTime/torchDuration;
                float lightValue2 = Mathf.Lerp(0.29f, 0.01f, counterClock2);
                torch2.intensity = lightValue2;
                yield return null;
            }

            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                Quaternion itemRotation= itemHolding.transform.rotation;
            Instantiate(destroyEffect, itemHolding.transform.position, itemRotation);
            //Instantiate(destroyEffectDebris, itemHolding.transform.position, itemRotation); it sucks
            //Destroy(itemHolding);
        }
    }

    private void torchDeathOverTime()
    {
        if (picked == true)
        {
        lifeTime = torchDuration;

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

