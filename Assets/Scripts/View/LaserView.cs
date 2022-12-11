using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserView : MonoBehaviour
{
    public GameObject laserBody;

    private LaserController laserController;
    private bool isOpen = true;
    
    // Start is called before the first frame update
    void Start()
    {
        laserController = GameObject.Find("LaserController").GetComponent<LaserController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            isOpen = !isOpen;
            laserController.SwitchLaser(gameObject, isOpen, laserBody);
        }
    }
}
