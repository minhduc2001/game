using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manage : MonoBehaviour
{
    public Canvas canvasSetting;
    public Button btnSetting;
    public Button btnStartGame;
    public Button btnQuitGame;
    public Animator animator;
    
    private void Awake()
    {
        canvasSetting.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        btnSetting.onClick.AddListener(handleBtnSettingClick);
        btnStartGame.onClick.AddListener(handleStartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasSetting.gameObject.active)
        {
            btnSetting.interactable = true;
        }
    }

    void handleBtnSettingClick()
    {
        canvasSetting.gameObject.SetActive(true);
        btnSetting.interactable = false;
    }

    void handleStartGame()
    {
        StartCoroutine(loadGame());
    }

    IEnumerator loadGame()
    {
        animator.SetBool("Start", true);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(3);
    }
}
