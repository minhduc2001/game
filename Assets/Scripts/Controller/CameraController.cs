using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public new Transform camera;

    private CameraModel camModel;
    
    // Start is called before the first frame update
    void Start()
    {
        camModel = GameObject.Find("CameraModel").GetComponent<CameraModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTracking(){
        float newX, newY;
        newX = target.position.x;
        newY = target.position.y;
        if(camera.position != target.position){
            newX = Mathf.Clamp(
                newX,
                camModel.limitLeft,
                camModel.limitRight
            );
            newY = Mathf.Clamp(
                newY,
                camModel.limitBottom,
                camModel.limitTop
            );
            Vector3 newPosition = new Vector3(
                newX, newY,
                camera.position.z
            );
            camera.position = Vector3.Lerp(
                camera.position,
                newPosition,
                camModel.smoothing
            );
        }
    }
}
