using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
public class Bubble : MonoBehaviour
{
    public Image bubble;
    public Sprite thought;
    public Sprite outLoud;
    // Start is called before the first frame update
    void Start()
    {
        bubble = GetComponentInParent<Image>();    
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
