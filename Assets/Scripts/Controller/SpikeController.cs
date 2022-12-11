using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private SpikeModel spikeModel;

    // Start is called before the first frame update
    void Start()
    {
        spikeModel = GameObject.Find("SpikeModel").GetComponent<SpikeModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public KeyValuePair <int, int> MoveSpike(Rigidbody2D body, int pos, int dirX){
        if(pos == spikeModel.limitRight)dirX = -1;
        if(pos == spikeModel.limitLeft) dirX = 1;
        body.velocity = new Vector2(spikeModel.speed * dirX, body.velocity.y);
        pos += dirX;
        return new KeyValuePair <int, int>(pos, dirX);
    }
}
