using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private CanvasModel canvasModel;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasModel = GameObject.Find("CanvasModel").GetComponent<CanvasModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectKey(string str){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory[str]++;
        canvasModel.SetInventory(inventory);
        GameObject.Find(str + " (1)").GetComponent<Image>().enabled = false;
        GameObject.Find(str + " (0)").GetComponent<Image>().enabled = true;
    }

    public void CollectCoin(){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory["coinGold"]++;
        canvasModel.SetInventory(inventory);
        // modify text
        GameObject.Find("coinText").GetComponent<Text>().text = inventory["coinGold"].ToString();
    }

    public void CollectGem(){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory["gemBlue"]++;
        canvasModel.SetInventory(inventory);
        // modify text
        GameObject.Find("gemText").GetComponent<Text>().text = inventory["gemBlue"].ToString();
    }

    public void DecreaseHeart(){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory["heart"]--;
        GameObject.Find("heart" + inventory["heart"].ToString()).GetComponent<Image>().enabled = false;
        if(inventory["heart"] == 0) {
            Debug.Log("game over");
        }
        canvasModel.SetInventory(inventory);
    }

    public bool IncreaseHeart(){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory["heart"]++;
        if(inventory["heart"] > 3) {
            inventory["heart"] = 3;
            return false;
        }
        Debug.Log(inventory["heart"]);
        GameObject.Find("heart" + (inventory["heart"]-1).ToString()).GetComponent<Image>().enabled = true;
        if(inventory["heart"] == 0) {
            Debug.Log("game over");
        }
        canvasModel.SetInventory(inventory);
        return true;
    }
}
