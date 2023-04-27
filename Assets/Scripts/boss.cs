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
        bossAnim = GetComponent<Animator>();

        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }

        if (transform.hasChanged)
        {
            bossAnim.SetBool("isChasing", true);
            transform.hasChanged = false;
        }
        else
        {
            bossAnim.SetBool("isChasing", false);
        }
    }
}


















