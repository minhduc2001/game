using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;
    private CanvasController canvasController;
    private SpringController springController;
    private LaserController laserController;
    private LockController lockController;
    private EndGameController endGameController;
    private PauseController pauseController;
    private SoundController soundController;
    // private AssetBundle myLoadedAssetBundle;
    // private string[] scenePaths;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
        springController = GameObject.Find("SpringController").GetComponent<SpringController>();
        laserController = GameObject.Find("LaserController").GetComponent<LaserController>();
        lockController = GameObject.Find("LockController").GetComponent<LockController>();
        endGameController = GameObject.Find("EndGameController").GetComponent<EndGameController>();
        // canvasController.LoadInventoryFromPrefs();
        // myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        pauseController = GameObject.Find("PauseController").GetComponent<PauseController>();
        soundController = GameObject.Find("AudioManager").GetComponent<SoundController>();
        // scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }

    // Update is called once per frame
    void Update()
    {
        // canvasController.LoadInventoryFromPrefs();
        float dirX = Input.GetAxisRaw("Horizontal");
        playerController.PlayerWalk(dirX);

        float dirY = Input.GetAxisRaw("Vertical");
        playerController.PlayerClimb(dirY);
        
        if (Input.GetButtonDown("Jump")){
            if(!lockController.GetUnlocking()) playerController.PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseController.GetGameIsPaused()){
                pauseController.ResumeGame();
            } else {
                pauseController.PauseGame();
            }
        }

        playerController.CheckDeathFallen(transform);
    }

    void OnCollisionEnter2D(Collision2D col){
        playerController.SetIsJumping(false);
        if(col.gameObject.CompareTag("spring")){
            // soundController.Play("Spring");
            playerController.SetIsJumping(true);
            springController.AddSpringForce(col, playerController.GetPlayerBody());
        }
    }

    void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.name == "Terrain"){
            // Debug.Log(transform.position);
            playerController.SetRebornPlace(transform.position);
        }
        if(col.gameObject.CompareTag("spike")){
            if(playerController.HurtByEnemy()){
                soundController.Play("Death");
                canvasController.DecreaseHeart();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("ladder")){
            playerController.SetIsClimbing(true);
        }
        if(col.gameObject.CompareTag("heart")){
            if(canvasController.IncreaseHeart()) {
                playerController.DestroyItem(col);
                soundController.Play("Item");
            }
        }
        if(col.gameObject.CompareTag("key")){
            soundController.Play("Item");
            canvasController.CollectKey(col.gameObject.name);
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("coin")){
            soundController.Play("Item");
            canvasController.CollectCoin();
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("gem")){
            soundController.Play("Item");
            canvasController.CollectGem();
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("level2")){
            canvasController.SaveInventoryToPrefs();SaveGameToFile(new Vector3(-7, -2, 0));
            Debug.Log("lv2");
            ChangeScene("level2");
            // SceneManager.LoadScene("level2", LoadSceneMode.Single);
        }
        if(col.gameObject.CompareTag("level3")){
            canvasController.SaveInventoryToPrefs();SaveGameToFile(new Vector3(-7, -2, 0));
            Debug.Log("lv3");
            SceneManager.LoadScene("level3", LoadSceneMode.Single);
        }
        if(col.gameObject.CompareTag("level4")){
            canvasController.SaveInventoryToPrefs();SaveGameToFile(new Vector3(-7, -2, 0));
            Debug.Log("lv4");
            SceneManager.LoadScene("level4", LoadSceneMode.Single);
        }
        // if(col.gameObject.CompareTag("winner")){
        //     canvasController.ResetInventory();
        //     SceneManager.LoadScene("winner", LoadSceneMode.Single);
        // }
        if(col.gameObject.CompareTag("spike")){
            soundController.Play("Death");
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("spike")){
            if(playerController.HurtByEnemy()){
                canvasController.DecreaseHeart();
            }
        }
        // if(col.gameObject.CompareTag("lock")){
        //     unlocking = true;
        //     Debug.Log(unlocking);
        // }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("ladder")){
            playerController.SetIsClimbing(false);
        }
    }
//
    private void ChangeScene(string str){
        StartCoroutine(loadScene(str));
    }

    IEnumerator loadScene(string str)
    {
        GameObject.Find("Crossfade").GetComponent<Animator>().SetBool("Start", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(str, LoadSceneMode.Single);
    }
//
    private void SaveGameToFile(Vector3 v){
        SaveModel saveModel = new SaveModel();
        saveModel.setInventory(canvasController.GetCanvasModel().GetInventory());
        saveModel.setPosition(v);
        saveModel.setLevel(SceneManager.GetActiveScene().buildIndex);
        SaveController.SaveGameObject(saveModel);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit game");
        if(canvasController.GetCanvasModel().GetInventory()["heart"] == 0) return;
        SaveGameToFile(playerController.GetPlayerBody().position);
        //
        canvasController.ResetInventory();
    }
}
