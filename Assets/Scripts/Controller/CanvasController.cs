using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private static CanvasModel canvasModel;

    private Dictionary<string, int> tempInventory;
    private EndGameController endGameController;
    private PauseController pauseController;

    public CanvasModel GetCanvasModel()
    {
        return canvasModel;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        endGameController = GameObject.Find("EndGameController").GetComponent<EndGameController>();
        canvasModel = GameObject.Find("CanvasModel").GetComponent<CanvasModel>();
        LoadInventoryFromPrefs();
        //
        tempInventory = canvasModel.GetInventory();
        if (SaveController.LoadSaveGame() != null)
        {
            foreach(string key in SaveController.LoadSaveGame().getInventory().Keys)
            {
                Debug.Log(key + " " + SaveController.LoadSaveGame().getInventory()[key]);
                tempInventory[key] = SaveController.LoadSaveGame().getInventory()[key];

            }

            canvasModel.SetInventory(tempInventory);
            InitData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitData()
    {
        tempInventory = canvasModel.GetInventory();
        foreach (string key in tempInventory.Keys)
        {
            if (key.Contains("key") && tempInventory[key] > 0)
            {
                GameObject.Find(key + " (1)").GetComponent<Image>().enabled = false;
                GameObject.Find(key + " (0)").GetComponent<Image>().enabled = true;
            } else 
            {
                if (key.Contains("heart"))
                {
                    // GameObject.Find("heart" + (tempInventory["heart"] - 1).ToString()).GetComponent<Image>().enabled = true;
                    for(int i = 2; i >= tempInventory["heart"]; --i){
                        GameObject.Find("heart" + i.ToString()).GetComponent<Image>().enabled = false;
                    }
                } else if (key.Contains("gem"))
                {
                    GameObject.Find("gemText").GetComponent<Text>().text = tempInventory[key].ToString();
                } else
                {
                    GameObject.Find("coinText").GetComponent<Text>().text = tempInventory["coinGold"].ToString();
                }
            }
        }
    }

    public void CollectKey(string str){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory[str]++;
        canvasModel.SetInventory(inventory);
        if(inventory[str] > 0){
            GameObject.Find(str + " (1)").GetComponent<Image>().enabled = false;
            GameObject.Find(str + " (0)").GetComponent<Image>().enabled = true;
        }
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
        for(int i = 2; i >= inventory["heart"]; --i){
            GameObject.Find("heart" + i.ToString()).GetComponent<Image>().enabled = false;
        }
        // GameObject.Find("heart" + inventory["heart"].ToString()).GetComponent<Image>().enabled = false;
        if(inventory["heart"] == 0) {
            Debug.Log("game over");
            pauseController.PauseGame();
            endGameController.showUiEnd();
            ResetInventory();
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
        // foreach(KeyValuePair<string, int> entry in inventory)
        // {
        //     // do something with entry.Value or entry.Key
        //     Debug.Log(entry.Value);
        //     Debug.Log(entry.Key);
        // }
        // Debug.Log("heart" + (inventory["heart"]-1).ToString());
        GameObject.Find("heart" + (inventory["heart"]-1).ToString()).GetComponent<Image>().enabled = true;
        canvasModel.SetInventory(inventory);
        return true;
    }

    public void SaveInventoryToPrefs(){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        PlayerPrefs.SetInt("keyBlue", inventory["keyBlue"]);
        PlayerPrefs.SetInt("keyRed", inventory["keyRed"]);
        PlayerPrefs.SetInt("keyYellow", inventory["keyYellow"]);
        PlayerPrefs.SetInt("keyGreen", inventory["keyGreen"]);
        PlayerPrefs.SetInt("coinGold", inventory["coinGold"]);
        PlayerPrefs.SetInt("gemBlue", inventory["gemBlue"]);
        PlayerPrefs.SetInt("heart", inventory["heart"]);        
        PlayerPrefs.Save();
    }

    public void LoadInventoryFromPrefs(){
        Dictionary<string, int> inventory = new Dictionary<string, int>();
        inventory.Add("keyBlue", PlayerPrefs.GetInt("keyBlue",0)-1);
        inventory.Add("keyRed", PlayerPrefs.GetInt("keyRed",0)-1);
        inventory.Add("keyYellow", PlayerPrefs.GetInt("keyYellow",0)-1);
        inventory.Add("keyGreen", PlayerPrefs.GetInt("keyGreen",0)-1);
        inventory.Add("coinGold", PlayerPrefs.GetInt("coinGold",0)-1);
        inventory.Add("gemBlue", PlayerPrefs.GetInt("gemBlue",0)-1);
        inventory.Add("heart", PlayerPrefs.GetInt("heart",3)+1);    
        // foreach(KeyValuePair<string, int> entry in inventory)
        // {
        //     // do something with entry.Value or entry.Key
        //     Debug.Log(entry.Value);
        //     Debug.Log(entry.Key);
        // } 
        canvasModel.SetInventory(inventory);
        CollectKey("keyBlue");
        CollectKey("keyRed");
        CollectKey("keyYellow");
        CollectKey("keyGreen");
        CollectCoin();
        CollectGem();
        DecreaseHeart();
    }

    public void ResetInventory(){
        PlayerPrefs.SetInt("keyBlue", 0);
        PlayerPrefs.SetInt("keyRed", 0);
        PlayerPrefs.SetInt("keyYellow", 0);
        PlayerPrefs.SetInt("keyGreen", 0);
        PlayerPrefs.SetInt("coinGold", 0);
        PlayerPrefs.SetInt("gemBlue", 0);
        PlayerPrefs.SetInt("heart", 3);  
    }
}
