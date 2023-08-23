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
    public QuestStep unlockedIfQuest;
    
    private QuestManager questManager;
    public void BaseInteract(){
        if(unlockedIfQuest != null && questManager.completeQuests.IndexOf(unlockedIfQuest)==-1) return; 
        // if the unlocked quest isn't null & isn't a completed quest then do not do the interaction
        
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
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
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
