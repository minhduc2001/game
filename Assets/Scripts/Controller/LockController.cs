using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LockController : MonoBehaviour
{
    public GameObject[] dialogImages;
    public GameObject[] dialogTexts;
    public GameObject wave;
    public GameObject ufo;
    public GameObject alien;
    public GameObject player, lockview;
    public GameObject invicibleWall;

    public Animator animator;

    private CanvasController canvasController;
    private CanvasModel canvasModel;

    private bool unlocking = false;
    // Start is called before the first frame update
    void Start()
    {
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
        canvasModel = GameObject.Find("CanvasModel").GetComponent<CanvasModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUnlocking(bool v){unlocking = v;}

    public bool GetUnlocking(){return unlocking;}

    public void UnlockPrison(string keyName){
        Dictionary<string, int> inventory = canvasModel.GetInventory();
        if(inventory["key" + keyName] == 0) {
            // 0
            dialogTexts[0].GetComponent<Text>().enabled = true;
            DialogImagesOn();
            Invoke("DialogImagesOff", 3f);
            Invoke("Text0Off", 3f);
            Debug.Log("need " +keyName+ " key");
        }
        else {
            Debug.Log("unlocked");
            wave.GetComponent<Animator>().SetBool("locked", false);
            player.GetComponent<PlayerView>().enabled = false;
            lockview.GetComponent<LockView>().enabled = false;
            // 1
            dialogTexts[1].GetComponent<Text>().enabled = true;
            DialogImagesOn();
            Invoke("DialogImagesOff", 15f);
            Invoke("Text1Off", 5f);
        }
    }

    private void DialogImagesOn(){
        for(int i = 0; i < dialogImages.Length; ++i){
            dialogImages[i].GetComponent<Image>().enabled = true;
        }
    }

    private void DialogImagesOff(){
        for(int i = 0; i < dialogImages.Length; ++i){
            dialogImages[i].GetComponent<Image>().enabled = false;
        }
    }

    private void Text0Off(){
        dialogTexts[0].GetComponent<Text>().enabled = false;
    }

    private void Text1Off(){
        dialogTexts[1].GetComponent<Text>().enabled = false;
        dialogTexts[2].GetComponent<Text>().enabled = true;
        Invoke("Text2Off", 5f);
    }

    private void Text2Off(){
        dialogTexts[2].GetComponent<Text>().enabled = false;
        dialogTexts[3].GetComponent<Text>().enabled = true;
        Invoke("Text3Off", 5f);
    }

    private void Text3Off(){
        dialogTexts[3].GetComponent<Text>().enabled = false;
        ufo.GetComponent<Animator>().enabled = true;
        alien.GetComponent<Animator>().SetBool("escaping", true);
        if(SceneManager.GetActiveScene().name == "level4"){
            Invoke("UFOTakesPlayer", 2.5f);
            Invoke("ChangeToTheEndScene", 6f);
        } 
        else Invoke("PlayerOn", 6f);
    }

    private void PlayerOn(){
        player.GetComponent<PlayerView>().enabled = true;
        invicibleWall.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void UFOTakesPlayer(){
        GameObject.Find("CameraController").GetComponent<CameraController>().enabled = false;
        player.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.transform.SetParent(alien.transform);
        player.transform.localPosition = new Vector3(1, 0, 0);
        
    }

    private void ChangeToTheEndScene(){
        StartCoroutine(loadTheEndScene());
    }

    IEnumerator loadTheEndScene()
    {
        animator.SetBool("Start", true);
        yield return new WaitForSeconds(1);
        GameObject.Find("EndGameController").GetComponent<EndGameController>().resetPlayerDotFun();
        SceneManager.LoadScene("TheEnd", LoadSceneMode.Single);
    }
}
