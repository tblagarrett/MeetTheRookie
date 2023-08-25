using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class I_Dialogue : Interactable
{
    public List<string> nodes;
    private int index = 0;
    private bool isCurrentConversation;
    private DialogueRunner dialogueRunner;

    public bool repeatLastDialogue;
    void Start(){
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    protected override void Interact(){
        StartDialogueLogic();
    }
    public void StartDialogueLogic(){
        if(index<nodes.Count){
            dialogueRunner.StartDialogue(nodes[index]);
            index++;
        }else if(repeatLastDialogue){
            dialogueRunner.StartDialogue(nodes[index]);
        }
    }
    /*
    private void StartConversation() {
        isCurrentConversation = true;
        // TODO *begin animation or turn on speaker indicator or whatever* HERE
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation() {
        if (isCurrentConversation) { 
            // TODO *stop animation or turn off indicator or whatever* HERE
            isCurrentConversation = false;
        }
    }
    */
}
