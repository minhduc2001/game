using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockView : MonoBehaviour
{
    private LockController lockController;
    public string keyName;

    // Start is called before the first frame update
    void Start()
    {
        lockController = GameObject.Find("LockController").GetComponent<LockController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            if(lockController.GetUnlocking()) lockController.UnlockPrison(keyName);
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            lockController.SetUnlocking(true);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            lockController.SetUnlocking(false);
        }
    }
}
