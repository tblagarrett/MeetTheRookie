using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Audio : Interactable
{
    public bool isLocked = false;
    string prefix = "((I_AUDIO)) ";

    protected override void Interact()
    {
        if(isLocked){
            Debug.Log("can't audio it locked");
            return;
        }
        Debug.Log(prefix + " Audio Trigger");
    }



}
