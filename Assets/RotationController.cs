using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    Transform r;
    public float fixedRotation = 180;

     void Start () {
        r = transform;
    }
    
     void Update () {
        r.eulerAngles = new Vector3 (r.eulerAngles.x, r.eulerAngles.y, fixedRotation);
    }
}