using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel playerModel;

    private Rigidbody2D body;
    private Animator anim;
    private bool isJumping = false;
    private bool isClimbing = false;
    private bool isInvincible = false;
    private float cntInvincible = 0;

    private Vector3 rebornPlace;
    
    private CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.Find("PlayerModel").GetComponent<PlayerModel>();
        body = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible){
            cntInvincible += Time.deltaTime;
            if(cntInvincible > 2){
                isInvincible = false;
                cntInvincible = 0f;
                anim.SetBool("death", false);
            }
        }
    }

    public Rigidbody2D GetPlayerBody(){return body;}

    public void PlayerWalk(float dirX){
        if(dirX != 0) {
            anim.SetFloat("moveX", dirX);
            anim.SetBool("moving", true);
        }
        else anim.SetBool("moving", false);
        body.rotation = 0f;
        body.velocity = new Vector2(
            dirX * playerModel.walkSpeed,
            body.velocity.y
        );
    }

    public void PlayerClimb(float dirY){
        if(!isClimbing) return;
        //anim
        if(dirY != 0) {
            anim.SetFloat("moveY", 1);
        }
        else {
            anim.SetFloat("moveY", -1);
        }
        body.rotation = 0f;
        body.velocity = new Vector2(body.velocity.x, dirY * playerModel.walkSpeed);
    }

    public void PlayerJump(){
        if(isJumping) return;
        if(isClimbing) return;
        body.velocity = new Vector2(body.velocity.x, playerModel.jumpSpeed);
        SetIsJumping(true);
    }

    public void SetIsJumping(bool val){
        isJumping = val;
        anim.SetBool("jumping", val);
    }

    public void SetIsClimbing(bool val){
        isClimbing = val;
        Debug.Log(isClimbing);
        anim.SetBool("climbing", val);
        if(isClimbing) {
            body.gravityScale = 0;
        }
        else {
            body.gravityScale = 3;
            anim.SetFloat("moveY", 0);
        }
        //
    }

    public void SetRebornPlace(Vector3 v){
        if(body.velocity.y == 0) rebornPlace = new Vector3(v.x, v.y+1f, v.z);
        // Debug.Log(rebornPlace);
    }

    public void CheckDeathFallen(Transform t){
        // Debug.Log(t.position);
        if(t.position.y < -15f){
            anim.SetBool("death", true);
            isInvincible = true;
            cntInvincible = 0f;
            t.position = rebornPlace;
            canvasController.DecreaseHeart();
        }
    }

    public void DestroyItem(Collider2D col){
        Destroy(col.gameObject);
    }

    public bool HurtByEnemy(){
        // Debug.Log("hurt");
        if(!isInvincible){
            anim.SetBool("death", true);
            isInvincible = true;
            cntInvincible = 0f;
            return true;
        }
        else return false;
    }
}
