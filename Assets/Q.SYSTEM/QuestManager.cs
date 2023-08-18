using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionManager))]
public class QuestManager : MonoBehaviour 
{
    [HideInInspector]
    public ActionManager actionManager;

    [Header("Quest Lines")]
    public List <QuestLine> activeQuestLines;

    public void Awake()
    {
        actionManager = GetComponent<ActionManager>();
    }

    public void UpdateActiveQuestLines()
    {
        foreach (QuestLine ql in activeQuestLines) { 
            Debug.Log("hiii " + ql.ID);


            if(ql){ ql.UpdateQuestLine(); }
        }
    }
}
