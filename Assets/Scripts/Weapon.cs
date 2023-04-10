using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage structure
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 , 11, 12, 13, 14, 15, 16, 17};
    public float[] pushForce = {2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f, 3.8f, 4.0f, 4.2f, 4.4f, 4.6f, 4.8f, 5.0f, 5.2f, 5.4f};

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;
    private float cooldown = 0.0f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        
        anim = GetComponent<Animator>();
    }

    protected override void Update()

    
    {
        base.Update();

        if((Input.GetKeyDown(KeyCode.Space)) || Input.GetMouseButtonDown(0))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
                return;

            //create new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
            
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
        
    }
    public void UpgradeWeapon()
    {
        weaponLevel = Clamp(weaponLevel + 1, 0, GameManager.instance.weaponSprites.Count - 1);
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    private int Clamp(int value, int min, int max)
    {
        if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
    }
        
/*     {   
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    } */

    public void SetWeaponLevel(int level)
    {
    weaponLevel = Mathf.Clamp(level, 0, GameManager.instance.weaponSprites.Count - 1);
    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
/*     {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    } */
}
