using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    GameObject gameObject { get; }
    public void Interact();
}

[RequireComponent(typeof(BoxCollider2D))]
public class Interactor : MonoBehaviour
{
    public Sprite icon;
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
