using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrivePlayer(Transform t, Collision2D col){
        col.gameObject.transform.SetParent(t);
    }

    public void UndrivePlayer(Collision2D col){
        col.gameObject.transform.SetParent(null);
    }
}
