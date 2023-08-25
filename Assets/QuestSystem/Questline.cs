using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLine : MonoBehaviour
{
    [HideInInspector]
    public QuestManager questManager;
    [HideInInspector]
    public ActionManager actionManager;
    public string description, title;
    public int ID, index = 0;

    [Header("Quest Steps")]
    public QuestStep currentQuest;

    [SerializeReference]
    public List<QuestStep> quests;

    public void Start()
    {
        questManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
        actionManager = questManager.actionManager;
        currentQuest = quests[0];
    }

    public void UpdateQuestLine()
    {
        bool satisfied = false;
        // if Fetch Quest check if items are in inventory
        if (currentQuest.questType == QuestType.FETCH)
        {
            // get ref to child version of currentQuest
            FetchQuestStep fetchQuestRef = currentQuest as FetchQuestStep;

            satisfied = actionManager.CheckFetchQuest(fetchQuestRef.fetchItems);
            Debug.Log(fetchQuestRef.name + " SATISFIED: " + satisfied);

            if (satisfied) { 
                
                questManager.completeQuests.Add(currentQuest);
                questManager.activeQuests.Remove(currentQuest);
                actionManager.ReadQuestComplete(fetchQuestRef.onComplete);
                if(index+1<quests.Count){
                    index++;
                    currentQuest = quests[index];  
                    questManager.activeQuests.Add(currentQuest);
                    
                }
            }
        }
        if(currentQuest.questType == QuestType.TALK_TO)
        {
        
        }
    }
}
