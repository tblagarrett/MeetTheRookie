using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StorageShelfMinigame : MonoBehaviour
{
    public GameObject box;
    public GameObject winScreen;
    public TMP_Text infoText;
    
    private Vector3 startCoords = new Vector3(-6.6f, 3.028f, 0);
    //x displacement: 4.4; y displacement: 2.25
    private Vector3 xDis = new Vector3(4.4f, 0, 0);
    private Vector3 yDis = new Vector3(0, -2.25f, 0);
    
    //Max xScale: 3; Max yScale: 1.8 
    private float minXScale = 2f;
    private float maxXScale = 3f;
    private float minYScale = 1.2f;
    private float maxYScale = 1.8f;
    
    // Selected Boxes
    private List<GameObject> boxes = new List<GameObject>();
    private List<Vector3> checkBoxes = new List<Vector3>();
    private List<GameObject> sBoxes = new List<GameObject>();
    private List<string> boxNames = new List<string>{"Strawberry Snack", "Chocolate Snack", "Banana Snack", "Vanilla Snack", "Strawberry Soda", "Chocolate Soda", "Banana Soda", "Vanilla Soda", "Strawberry Bar", "Chocolate Bar", "Banana Bar", "Vanilla Bar", "Strawberry Pudding", "Chocolate Pudding", "Banana Pudding", "Vanilla Pudding"};
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 boxCoords = startCoords;
        int count = 0;
        for(int i = 0; i<4; i++){
            for(int j = 0; j<4; j++){
                GameObject newBox = Instantiate(box, boxCoords, box.transform.rotation);
                newBox.transform.SetParent(this.transform);
                newBox.transform.GetChild(0).transform.localScale = new Vector3(Random.Range(minXScale, maxXScale), Random.Range(minYScale, maxYScale), 1);
                newBox.GetComponentInChildren<TMP_Text>().text = boxNames[count];
                boxes.Add(newBox);
                checkBoxes.Add(boxCoords);
                boxCoords += xDis;
                count++;
            }
            boxCoords.x = startCoords.x;
            boxCoords += yDis;
        }
        int swaps = Random.Range(4,7);
        Debug.Log("Swaps: "+swaps);
        for(int i=0; i<swaps; i++){
            int sbox1 = Random.Range(0, 16);
            int sbox2 = Random.Range(0, 16);
            Swap(boxes[sbox1], boxes[sbox2]);
            infoText.text += "Swapped "+boxNames[sbox1]+" and "+boxNames[sbox2]+".\n";
            Debug.Log("Swap "+i+": "+boxNames[sbox1]+", "+boxNames[sbox2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool win = true;
        for(int i = 0; i<16; i++){
            win &= (boxes[i].transform.position==checkBoxes[i]);
        }
        winScreen.SetActive(win);
    }
    
    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void SelectBox(GameObject sBox){
        if(sBoxes.Count>0){
            if(sBoxes.Contains(sBox)){
                sBoxes.Remove(sBox);
            }
            else{
                Swap(sBoxes[0], sBox);
                sBox.GetComponentInChildren<StorageShelfBox>().Select();
                sBoxes[0].GetComponentInChildren<StorageShelfBox>().Select();
                sBoxes.Remove(sBoxes[0]);
            }
        }
        else{
            sBoxes.Add(sBox);
        }
    }
    
    public void Swap(GameObject box1, GameObject box2){
        Vector3 tempBox = box1.transform.position;
        box1.transform.position = box2.transform.position;
        box2.transform.position = tempBox;
    }
    
    
}
