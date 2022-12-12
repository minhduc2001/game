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

        // myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        // scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        playerController.PlayerWalk(dirX);

        float dirY = Input.GetAxisRaw("Vertical");
        playerController.PlayerClimb(dirY);
        
        if (Input.GetButtonDown("Jump")){
            if(!lockController.GetUnlocking()) playerController.PlayerJump();
        }

        playerController.CheckDeathFallen(transform);
    }

    void OnCollisionEnter2D(Collision2D col){
        playerController.SetIsJumping(false);
        if(col.gameObject.CompareTag("spring")){
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
                canvasController.DecreaseHeart();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("ladder")){
            playerController.SetIsClimbing(true);
        }
        if(col.gameObject.CompareTag("heart")){
            if(canvasController.IncreaseHeart()) playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("key")){
            endGameController.updateCanvasModel(col.gameObject.name);
            endGameController.showUiEnd();
            canvasController.CollectKey(col.gameObject.name);
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("coin")){
            canvasController.CollectCoin();
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("gem")){
            canvasController.CollectGem();
            playerController.DestroyItem(col);
        }
        if(col.gameObject.CompareTag("level2")){
            SceneManager.LoadScene("level2", LoadSceneMode.Single);
        }
        if(col.gameObject.CompareTag("level3")){
            SceneManager.LoadScene("level3", LoadSceneMode.Single);
        }
        if(col.gameObject.CompareTag("level4")){
            SceneManager.LoadScene("level4", LoadSceneMode.Single);
        }
        if(col.gameObject.CompareTag("winner")){
            SceneManager.LoadScene("winner", LoadSceneMode.Single);
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
}
