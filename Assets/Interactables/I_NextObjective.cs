using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_NextObjective : Interactable
{
    public bool isLocked = false;
    
    protected override void Interact(){
        if(isLocked){
            Debug.Log("can't nextObjective it locked");
            return;
        }
    }
}