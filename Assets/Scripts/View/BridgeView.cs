using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeView : MonoBehaviour
{
    private BrigdeController brigdeController;
    
    // Start is called before the first frame update
    void Start()
    {
        brigdeController = GameObject.Find("BrigdeController").GetComponent<BrigdeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Player"))
            brigdeController.DrivePlayer(transform, col);
    }

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Player"))
            brigdeController.UndrivePlayer(col);
    }
}
