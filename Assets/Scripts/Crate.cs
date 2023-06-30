using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    public List<GameObject> corpseAnimation = new List<GameObject>();
    public Transform thisObject;
    private Vector3 thisObjectsPosition;
    protected override void Death()
    {
        thisObject=GetComponent<Transform>();
        thisObjectsPosition=thisObject.position;
        if (corpseAnimation.Count != 0)
        {
        Instantiate(corpseAnimation[Random.Range(0, corpseAnimation.Count)], thisObjectsPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
