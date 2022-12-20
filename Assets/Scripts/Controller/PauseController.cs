using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public Canvas canvasSetting;
    private bool gameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
        canvasSetting.enabled = true;
        Debug.Log("paused");
        canvasSetting.gameObject.SetActive(true);
        // canvasSetting.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame(){
        Debug.Log("resume");
        canvasSetting.gameObject.SetActive(false);
        // canvasSetting.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void SetGameIsPaused(bool val){
        gameIsPaused = val;
    }

    public bool GetGameIsPaused(){
        return gameIsPaused;
    }
}
