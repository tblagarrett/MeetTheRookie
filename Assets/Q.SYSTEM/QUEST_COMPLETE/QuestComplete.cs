using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestCompleteType { NONE, GIVE , MESSAGE , SEND_TO , MINIGAME}

[System.Serializable]
public abstract class QuestComplete : ScriptableObject
{
    public QuestCompleteType type = QuestCompleteType.NONE;
}
