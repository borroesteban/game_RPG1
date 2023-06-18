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
private Transform torchLight;
private Transform flickerTorch;
private Vector3 facingDirection;
private Vector3 target;
private Vector3 torchCurrentPosition;
private Light2D torch;
private BoxCollider2D torchColliderComponent;
private bool picked;
public bool executeThrow;
public float torchDuration;
private float lifeTime;
private float counterClock;
private float lightValue;
private float value;
public float speed;
public float distance;
public GameObject go;
Camera cam;



    //Update is called once per frame
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

    private void Start()
    {
        picked = false;
    }

    public void Awake()
    {
    cam = Camera.main;
    
    }

    void Update()
    {
        torchDeathOverTime();
        if(Input.GetMouseButton(1) && picked==true) 
        {
            torchCurrentPosition=flickerTorch.transform.position;
            flickerTorch.transform.parent = null;   
            getMousePosition();
            
            //
            //flickerTorch.transform.position = Vector3.LerpUnclamped (transform.position, target, speed);
            //speed=speed + 0.1f;   

            //transform.Translate(Vector3.Normalize(target - transform.position) * speed);   
            //speed=speed + 0.01f;
        }
        if (torchCurrentPosition != target && picked==true)
            {
                torchCurrentPosition = Vector3.Lerp (torchCurrentPosition, target, speed);
            }
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

/*     private void torchMovement()
    {
        if (picked == true)
            {
            flickerTorch.transform.position = Vector3.Lerp (transform.position, target, speed); //this works
            }
    } */

    private void getMousePosition()
    {
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(cam.transform.position.z);
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;
            target = mouseWorldPosition;
            Instantiate(go, mouseWorldPosition, Quaternion.identity);
    }
}


/*     private void torchThrow()
    {
        facingDirection=GameObject.Find("Player").transform.localScale;
        if(Input.GetMouseButton(1) && picked==true) 
        {
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(cam.transform.position.z);
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;
            target = mouseWorldPosition;
            Instantiate(go, mouseWorldPosition, Quaternion.identity);     
        }
    }
            
    private void torchMovement()
    {   
        if (executeThrow==true)
        {
        flickerTorch = playerTorchHolder.transform.GetChild(0);
        flickerTorch.transform.parent = null;
        flickerTorch.transform.position = Vector3.Lerp (transform.position, target, speed); //this works
        picked=false;
        }
    } */


            //flickerTorch.transform.position = Vector3.MoveTowards(flickerTorch.transform.position, target, distance*Time.deltaTime); //anda mas o menos
            //flickerTorch.transform.position = target; //this works
            //flickerTorch.transform.position = Vector3.Lerp (transform.position, target, speed); //this works
            //flickerTorch.transform.Translate(target*Time.deltaTime); //not working
 //flickerTorch.transform.Translate((new Vector3(distance,0,0)) * speed*Time.deltaTime);
/*             if (facingDirection.x < 0)
            {
                flickerTorch = playerTorchHolder.transform.GetChild(0);
                flickerTorch.transform.SetParent(GameObject.Find("torches").transform);
                flickerTorch.transform.Translate((new Vector3(-1,0,0) * _speed) * Time.deltaTime);
                picked=false;
            } */




