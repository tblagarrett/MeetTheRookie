using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public SceneManager sceneManager;
    public QuestManager questManager;
    public Inventory inventory;
    /*
    reference to:
    - player,
    - scene manager,
    - quest manager,
    */

    /*
    receive:  {item}
    delete: {item}
    clear inventory: {}
    sendPlayerTo: {location}
    enterMinigame: {minigame}
    enterCutscene: {cutscene}
    */

    public void Awake()
    {
        inventory = GetComponent<Inventory>();
        questManager = GetComponent<QuestManager>();
        sceneManager = GetComponent<SceneManager>();
    }

    #region actions
    public void RecieveItem(Item item){
        inventory.AddToInventory(item);
        questManager.UpdateActiveQuestLines();
    }



    #endregion


    #region check quests
    public bool CheckFetchQuest(List<Item> input_items)
    {
        return inventory.AreItemsInInventory(input_items);
    }
    public bool CheckTalkToQuest()
    {
        return true;
    }
    public bool CheckGoToQuest()
    {
        return true;
    }

    public bool CheckMinigameQuest()
    {
        return true;
    }

    #endregion


    #region quest complete
    public void ReadQuestComplete(QuestComplete questComplete)
    {
        //Debug.Log(questComplete.type);
        if(questComplete == null) return;
        if (questComplete.type == QuestCompleteType.SEND_TO)
        {
            Location_QuestComplete locationOnComplete = questComplete as Location_QuestComplete;
            Debug.Log("Action Manager: SEND TO " + locationOnComplete.locationType);
        }

        if (questComplete.type == QuestCompleteType.GIVE){
            Debug.Log("Action Manager: GIVE" );
        }

        if (questComplete.type == QuestCompleteType.MESSAGE){
            Debug.Log("Action Manager: MESSAGE " );
        }

        if (questComplete.type == QuestCompleteType.MINIGAME){
            Debug.Log("Action Manager: MESSAGE ");
        }
    }
    #endregion

}
