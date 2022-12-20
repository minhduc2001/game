using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    private SpringModel springModel;

    private Animator anim;

    void Start()
    {
        springModel = GameObject.Find("SpringModel").GetComponent<SpringModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpringForce(Collision2D col, Rigidbody2D playerBody){
        if(playerBody.position.y - col.gameObject.transform.position.y > 0.5f){
            playerBody.velocity = new Vector2(playerBody.velocity.x, springModel.speed);
            Debug.Log("add force");
            GameObject.Find("AudioManager").GetComponent<SoundController>().Play("Spring");
            anim = col.gameObject.GetComponent<Animator>();
            anim.SetBool("isActive", true);
            Invoke("SetSpringDown", 1);
        }
    }

    private void SetSpringDown(){
        anim.SetBool("isActive", false);
    }
}
