using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class EndGameController : MonoBehaviour
{
    private CanvasModel canvasModel;
    private GameObject canvas;
    private GameObject[] arrayKey;
    // private Button btnClose;
    private Button btnStart;
    // private Button btnSetting;
    private Canvas canvasSetting;

    private PauseController pauseController;

    private void Start()
    {
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();

        canvasModel = GameObject.Find("CanvasModel").GetComponent<CanvasModel>();
        canvas = GameObject.FindGameObjectWithTag("end");
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
        arrayKey = GameObject.FindGameObjectsWithTag("keyEnd");
        // btnClose = GameObject.Find("BtnCloseEnd").GetComponent<Button>();
        // btnSetting = GameObject.Find("BtnSettingEnd").GetComponent<Button>();
        btnStart = GameObject.Find("BtnRestartEnd").GetComponent<Button>();
        canvasSetting = GameObject.Find("SettingMenu").GetComponent<Canvas>();
        canvasSetting.enabled = false;

        // btnSetting.onClick.AddListener(openSetting);
        btnStart.onClick.AddListener(restartGame);
        // btnClose.onClick.AddListener(close);
    }

    void restartGame()
    {
        pauseController.ResumeGame();
        SceneManager.LoadScene(0);
    }

    void openSetting() {
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
        canvasSetting.enabled = true;
    }

    void close()
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void showUiEnd()
    {
        Dictionary<string, int> inventory = canvasModel.GetInventory();

        foreach (string key in inventory.Keys)
        {
            String name = key + "End" + " (1)";
            if (inventory[key] > 0 && key.Contains("key"))
            {
                GameObject.Find(name).GetComponent<Image>().enabled = true;
            }
        }
        // inventory.Clear();
        // inventory.Add("keyBlue", 0);
        // inventory.Add("keyRed", 0);
        // inventory.Add("keyYellow", 0);
        // inventory.Add("keyGreen", 0);
        // inventory.Add("coinGold", 0);
        // inventory.Add("gemBlue", 0);
        // inventory.Add("heart", 3);

        canvas.gameObject.GetComponent<Canvas>().enabled = true;
        //
        resetPlayerDotFun();
        Debug.Log("end");
        GameObject.Find("SettingMenu").SetActive(false);
        // SaveModel saveModel = new SaveModel();
        // saveModel.setInventory(inventory);
        // saveModel.setPosition(new Vector3(-7, -2, 0));
        // saveModel.setLevel(0);
        // SaveController.SaveGameObject(saveModel);
    }

    public void resetPlayerDotFun(){
        Dictionary<string, int> inventory = new Dictionary<string, int>();
        inventory.Add("keyBlue", 0);
        inventory.Add("keyRed", 0);
        inventory.Add("keyYellow", 0);
        inventory.Add("keyGreen", 0);
        inventory.Add("coinGold", 0);
        inventory.Add("gemBlue", 0);
        inventory.Add("heart", 3);
        canvasModel.SetInventory(inventory);
        SaveModel saveModel = new SaveModel();
        saveModel.setInventory(inventory);
        saveModel.setPosition(new Vector3(-7, -2, 0));
        saveModel.setLevel(0);
        SaveController.SaveGameObject(saveModel);
    }

    public void updateCanvasModel(string key)
    {
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory[key]++;

        canvasModel.SetInventory(inventory);
    }

}
