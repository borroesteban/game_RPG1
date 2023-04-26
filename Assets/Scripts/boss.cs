using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Enemy
{
    private Animator bossAnim;
    public float[] fireballSpeed = {2.5f, -2.5f};
    public float distance = 0.25f;
    public Transform[] fireballs;

    private void Update()
    {   
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }

    private void ischasing()
    {
        bossAnim = GetComponent<Animator>();
        if (chasing == true)
        {
        bossAnim.SetBool("isChasing", true);
        }
        if (chasing == false)
        {
        bossAnim.SetBool("isChasing", false);
        }
    }
    
}

