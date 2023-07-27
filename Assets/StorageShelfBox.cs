using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelfBox : MonoBehaviour
{
    public GameObject yellowHighlight;
    public GameObject greenHighlight;
    public StorageShelfMinigame shelf;
    
    public bool selected = false;
    
    private Color startColor;
    private Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        shelf = GetComponentInParent<StorageShelfMinigame>();
        originalColor = GetComponent<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseEnter()
    {
        yellowHighlight.SetActive(true);
        // startColor = GetComponent<SpriteRenderer>().material.color;
        // GetComponent<SpriteRenderer>().material.color += new Color(0,0.2f,0,0);
    }
    void OnMouseExit()
    {
        yellowHighlight.SetActive(false);
        // GetComponent<SpriteRenderer>().material.color = startColor;
    }
    private void OnMouseUp() {
        Select();
        shelf.SelectBox(this.transform.parent.transform.parent.gameObject);
        // Color trigColor = (startColor == originalColor) ? new Color(0,0.5f,0,0): originalColor;
        // startColor = trigColor;
        // GetComponent<SpriteRenderer>().material.color += trigColor;
    }
    
    public void Select(){
        selected = (selected) ? false : true;
        greenHighlight.SetActive(selected);
    }
}
