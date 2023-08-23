using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public bool useEvents; //add or remove InteractionEvent component to this gameobject
    public Sprite icon;

    public void BaseInteract(){
        if(useEvents){
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }
    protected virtual void Interact(){
        //only for children to override
    }

    private void Awake(){
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponentInChildren<Player_Interaction>().showIcon=true;
            collision.GetComponentInChildren<Player_Interaction>().interactIcon.GetComponent<SpriteRenderer>().sprite = icon;
            
            collision.GetComponentInChildren<Player_Interaction>().OpenInteractableIcon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponentInChildren<Player_Interaction>().CloseInteractableIcon();
            collision.GetComponentInChildren<Player_Interaction>().showIcon=false;
        }
    }
    private void PickUp(){
        Destroy(gameObject);
    }
}
