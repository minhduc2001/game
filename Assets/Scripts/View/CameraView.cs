using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    private CameraController camController;
    
    // Start is called before the first frame update
    void Start()
    {
        camController = GameObject.Find("CameraController").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        camController.PlayerTracking();
    }
}
