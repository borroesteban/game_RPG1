using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class pickUpTorch : MonoBehaviour
{
public float torchDuration;
public float onDeathRotation;
public float motionSpeed;
public float rotateSpeed;
public GameObject destroyEffect;
private BoxCollider2D torchColliderComponent;
private GameObject itemHolding;
private Transform torchTransformComponent;
private Transform playerTorchHolder;
private Transform flickerTorch;
private Transform torchToKill;
private Light2D torch;
private Vector3 target;
Camera cam;
private bool picked;
private float lifeTime;
private float counterClock;
private float lightValue;

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
            torchToKill = torchTransformComponent;
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

            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                Quaternion itemRotation= itemHolding.transform.rotation;
                Instantiate(destroyEffect, itemHolding.transform.position, itemRotation);
        }
    }

    private void torchDeathOverTime()
    {
    if (picked == true)
        {
            lifeTime = torchDuration;
            flickerTorch = torchToKill.GetChild(0);
            torch = flickerTorch.GetComponent<Light2D>();
            counterClock += Time.deltaTime / torchDuration;
            lightValue = Mathf.Lerp(0.29f, 0.01f, counterClock);
            torch.intensity = lightValue;
            Destroy(gameObject, lifeTime);
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

