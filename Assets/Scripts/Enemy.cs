using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //public GameObject corpse;
    public List<GameObject> corpseAnimation = new List<GameObject>();
    //experience
    public int xpValue = 1;

    // logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    public Transform thisTransform;
    private Vector3 positionCheckOrigin;
    private Vector3 positionCheckActual;
    public Animator animator;

    
    
    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        positionCheckOrigin=thisTransform.position;
    }

    private void FixedUpdate()
    {
        positionCheckActual=thisTransform.position;
        checkMovement();

          //is the player in range?
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;
                
            if (chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
            
            
        }

        //check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //the array is not cleaned up, so we do it ourselves
            hits[i] = null;
        }
    }



    protected override void Death()
    {
        int randomAnimation = Random.Range(0, corpseAnimation.Count);
        Instantiate(corpseAnimation[randomAnimation], positionCheckActual, Quaternion.identity);
        //Instantiate(corpse, positionCheckActual, Quaternion.identity);
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }

    private void checkMovement()
    {
        if (positionCheckOrigin != positionCheckActual)
        {
            animator.SetTrigger("isMoving");
        }
        if (positionCheckOrigin == positionCheckActual)
        {
            animator.SetTrigger("isNotMoving");
        }
    }
}



