using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CashRegisterMinigame : MonoBehaviour
{
    public List<Button> cashButtons;
    public GameObject winScreen;
    public List<string> cashRegisterSymbols = new List<string>{"1", "2", "3", "DEL", "4", "5", "6", "SUM", "7", "8", "9", "ENTER", "0", "00", ".", "CASH"};
    
    private List<HashSet<int>> activateList = new List<HashSet<int>>();
    private List<HashSet<int>> deactivateList = new List<HashSet<int>>();
    
    private List<int> butNum = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
    
    // Start is called before the first frame update
    void Start()
    {
        List<int> buttonChain = UniqRandIntList(4, 8);
        // buttonChain.Sort();
        for(int i = 0; i<butNum.Count; i++){
            deactivateList.Add(new HashSet<int>());
        }
        for(int i = 0; i<butNum.Count; i++){
            activateList.Add(new HashSet<int>());
        }
        SetInactive(buttonChain[0], Random.Range(4,8));
        SetDeactive(buttonChain);
        // CreateChain(buttonChain);
        CreateLinkedChain(buttonChain);
        // Debug deactivateList
        /* foreach(HashSet<int> l in deactivateList){
            string listo = "";
            foreach(int num in l){
                listo += cashRegisterSymbols[num];
                listo += " ";
            }
            Debug.Log(listo);
        } */
        MakeCheatSheet(buttonChain);
    }
    
    // Update is called once per frame
    void Update()
    {
        bool win = true;
        foreach(Button b in cashButtons){
            win &= b.interactable;
        }
        if(win){
            winScreen.SetActive(true);
        }
    }
    
    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void CreateChain(List<int> buttonChain){
        List<int> completeChain = new List<int>(butNum);
        foreach(int b in buttonChain){
            completeChain.Remove(b);
        }
        for(int i = 0; i<buttonChain.Count-1; i++){
            if(i==0){
                activateList[buttonChain[i]].Add(buttonChain[i]);
                cashButtons[i].interactable = true;
            }
            activateList[buttonChain[i]].Add(buttonChain[i+1]);
            for(int j = i; j<buttonChain.Count; j++){
                deactivateList[buttonChain[j]].Remove(i);
            }
        }
        activateList[buttonChain[buttonChain.Count-1]].Add(buttonChain[buttonChain.Count-1]);
        cashButtons[buttonChain.Count-1].interactable = true;
        foreach(int b in completeChain){
            int addB = Random.Range(0, buttonChain.Count);
            activateList[buttonChain[addB]].Add(b);
            for(int i = addB; i<buttonChain.Count; i++){
                deactivateList[buttonChain[i]].Remove(b);
            }
        }
        // for(int i = 1; i<buttonChain.Count-1; i++){
        //     int addB = Random.Range(i+1, buttonChain.Count);
        //     activateList[buttonChain[addB]].Add(i);
        //     for(int j = addB; j<buttonChain.Count; j++){
        //         deactivateList[buttonChain[j]].Remove(i);
        //     }
        // }
    }
    
    private void CreateLinkedChain(List<int> buttonChain){
        List<int> completeChain = new List<int>(butNum);
        foreach(int b in buttonChain){
            completeChain.Remove(b);
        }
        for(int i = 0; i<buttonChain.Count-1; i++){
            if(i==0){
                activateList[buttonChain[i]].Add(buttonChain[i]);
                cashButtons[i].interactable = true;
            }
            activateList[buttonChain[i]].Add(buttonChain[i+1]);
            activateList[buttonChain[i+1]].Add(buttonChain[i]);
            for(int j = i; j<buttonChain.Count; j++){
                deactivateList[buttonChain[j]].Remove(i);
            }
        }
        activateList[buttonChain[buttonChain.Count-1]].Add(buttonChain[buttonChain.Count-1]);
        cashButtons[buttonChain.Count-1].interactable = true;
        foreach(int b in completeChain){
            int addB = Random.Range(0, buttonChain.Count);
            activateList[buttonChain[addB]].Add(b);
            for(int i = addB; i<buttonChain.Count; i++){
                deactivateList[buttonChain[i]].Remove(b);
            }
        }
        // for(int i = 1; i<buttonChain.Count-1; i++){
        //     int addB = Random.Range(i+1, buttonChain.Count);
        //     activateList[buttonChain[addB]].Add(i);
        //     for(int j = addB; j<buttonChain.Count; j++){
        //         deactivateList[buttonChain[j]].Remove(i);
        //     }
        // }
    }
    
    private void SetInactive(int exclude, int num){
        List<int> numList = new List<int>(butNum);
        numList.Remove(exclude);
        for(int i=0; i<num; i++){
            int randNum = Random.Range(0, numList.Count);
            cashButtons[numList[randNum]].interactable = false;
            numList.RemoveAt(randNum);
        }
    }
    
    private void SetDeactive(List<int> buttonChain){
        List<int> numList = new List<int>(butNum);
        numList.Remove(buttonChain[0]);
        numList.Remove(buttonChain[buttonChain.Count-1]);
        for(int i = 0; i<numList.Count; i++){
            for(int j = 0; j<Random.Range(0, 3); j++){
                deactivateList[numList[i]].Add(numList[Random.Range(0, numList.Count)]);
            }
        }
    }
    
    private void MakeCheatSheet(List<int> buttonChain){
        string cheatSheet = "";
        foreach(int b in buttonChain){
            cheatSheet += cashRegisterSymbols[b];
            cheatSheet += " ";
        }
        Debug.Log("Cheat Sheet: "+cheatSheet);
    }
    
    private List<int> UniqRandIntList(int low, int high){
        List<int> randList = new List<int>();
        List<int> numList = new List<int>(butNum);
        int numGen = Random.Range(Mathf.Max(low, 1), high+1);
        
        for(int i = 0; i<numGen; i++){
            int randNum = Random.Range(0, numList.Count);
            randList.Add(numList[randNum]);
            numList.RemoveAt(randNum);
        }
        // randList.Sort();
        return randList;
    }
    
    public void Push(int ind){
        cashButtons[ind].interactable = false;
        if(deactivateList.Count>0){
            foreach(int b in deactivateList[ind]){
                cashButtons[b].interactable = false;
            }
        }
        if(activateList.Count>0){
            foreach(int b in activateList[ind]){
                cashButtons[b].interactable = true;
            }
        }
    }
}
