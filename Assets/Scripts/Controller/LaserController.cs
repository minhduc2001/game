using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLaser(GameObject laserButton, bool isOpen, GameObject laserBody){
        laserButton.GetComponent<Animator>().SetBool("isOpen", isOpen);
        laserBody.GetComponent<SpriteRenderer>().enabled = isOpen;
        laserBody.GetComponent<Animator>().enabled = isOpen;
        laserBody.GetComponent<BoxCollider2D>().enabled = isOpen;
    }
}
