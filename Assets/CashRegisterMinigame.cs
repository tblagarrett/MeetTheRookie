using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashRegisterMinigame : MonoBehaviour
{
    public List<Button> cashButtons;
    public GameObject winScreen;
    
    private List<List<int>> activateList = new List<List<int>>();
    private List<List<int>> deactivateList = new List<List<int>>();
    
    private List<int> ButNum = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
    
    // Start is called before the first frame update
    void Start()
    {
        List<int> buttonChain = UniqRandIntList(4, 8);
        // buttonChain.Sort();
        for(int i = 0; i<16; i++){
            deactivateList.Add(new List<int>());
        }
        for(int i = 0; i<16; i++){
            activateList.Add(new List<int>());
        }
        SetInactive(buttonChain[0], Random.Range(4,8));
        
        CreateChain(buttonChain);
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
    
    void CreateChain(List<int> buttonChain){
        List<int> completeChain = new List<int>(ButNum);
        Debug.Log("Button Chain: "+buttonChain);
        foreach(int b in buttonChain){
            completeChain.Remove(b);
        }
        for(int i = 0; i<buttonChain.Count-1; i++){
            if(i==0){
                activateList[buttonChain[i]].Add(buttonChain[i]);
                cashButtons[i].interactable = true;
            }
            activateList[buttonChain[i]].Add(buttonChain[i+1]);
        }
        foreach(int b in completeChain){
            int addB = Random.Range(0, buttonChain.Count);
            activateList[buttonChain[addB]].Add(b);
            for(int i = addB; i<buttonChain.Count; i++){
                deactivateList[i].Remove(b);
            }
        }
    }
    
    void SetInactive(int exclude, int num){
        List<int> numList = new List<int>(ButNum);
        numList.Remove(exclude);
        for(int i=0; i<num; i++){
            int randNum = Random.Range(0, numList.Count);
            cashButtons[numList[randNum]].interactable = false;
            numList.RemoveAt(randNum);
        }
    }
    
    List<int> UniqRandIntList(int low, int high){
        List<int> randList = new List<int>();
        List<int> numList = new List<int>(ButNum);
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
