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
    private Button btnClose;
    private Button btnStart;
    private Button btnSetting;
    private Canvas canvasSetting;

    private void Start()
    {
        canvasModel = GameObject.Find("CanvasModel").GetComponent<CanvasModel>();
        canvas = GameObject.FindGameObjectWithTag("end");
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
        arrayKey = GameObject.FindGameObjectsWithTag("keyEnd");
        btnClose = GameObject.Find("BtnCloseEnd").GetComponent<Button>();
        btnSetting = GameObject.Find("BtnSettingEnd").GetComponent<Button>();
        btnStart = GameObject.Find("BtnRestartEnd").GetComponent<Button>();
        canvasSetting = GameObject.Find("SettingMenu").GetComponent<Canvas>();
        canvasSetting.enabled = false;

        btnSetting.onClick.AddListener(openSetting);
        btnStart.onClick.AddListener(restartGame);
        btnClose.onClick.AddListener(close);
    }

    void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        canvas.gameObject.GetComponent<Canvas>().enabled = true;

    }

    public void updateCanvasModel(string key)
    {
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        inventory[key]++;

        canvasModel.SetInventory(inventory);
    }

}
