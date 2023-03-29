using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite chest_regular_0;
    public int pesosAmount = 5;
    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = chest_regular_0;
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!",25,Color.yellow,transform.position,Vector3.up * 25, 1.5f);
        }
    }
}
