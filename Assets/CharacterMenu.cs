using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;


    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    
    //character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            //if we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            currentCharacterSelection = 0;
            OnselectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            //if we went too far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            OnselectionChanged();
        }
    }
    private void OnselectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    //weapon upgrade
    public void OnUpgradeClick()
    {
        //
    }

    //update the character information
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        upgradeCostText.text = "not implemented yet";

        //meta
        levelText.text = "Not implemented yet";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        //xp bar

        xpText.text = "not implemented yet";
        xpBar.localScale = new Vector3(0.5f, 0, 0);

    }

}
