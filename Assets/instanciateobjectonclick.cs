using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciateobjectonclick : MonoBehaviour
{
    public GameObject go;
    Camera cam;


    public void Awake()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(cam.transform.position.z);
            //worldPoint.z = 11f;
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;
            Instantiate(go, mouseWorldPosition, Quaternion.identity);
        }
    }
}

