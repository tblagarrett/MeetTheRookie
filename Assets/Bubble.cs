using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Image bubble;

    // Start is called before the first frame update
    void Start()
    {
        bubble = GetComponent<Image>();    
    }

    [YarnCommand("thought")]
    public void Thought(){
        bubble.sprite = thought;
    }
    [YarnCommand("out loud")]
    public void OutLoud(){
        bubble.sprite = outLoud;
    }
}
