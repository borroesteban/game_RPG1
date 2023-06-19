using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtMousePos : MonoBehaviour
{
    private Vector3 target;
    public GameObject go;
    Camera cam;
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
