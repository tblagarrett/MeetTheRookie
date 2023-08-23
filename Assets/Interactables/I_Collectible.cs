using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(SpriteRenderer))]
public class I_Collectible : Interactable
{
    private ActionManager actionManager;

    public bool locked = false;
    public Item item;

    void Start()
    {
        actionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ActionManager>();
    }

    protected override void Interact()
    {   
        if(locked){
            Debug.Log("can't pick up the collectible! it locked");
            //ADD "lockedText" HERE!
            return;
        }

        //ADD LOGIC FOR POPUP
        actionManager.RecieveItem(item);

        //Destroy(this.gameObject);
    }

}
