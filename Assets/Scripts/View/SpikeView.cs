using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeView : MonoBehaviour
{
    private SpikeController spikeController;

    private Rigidbody2D body;

    private int pos = 0, dirX = 1;

    // Start is called before the first frame update
    void Start()
    {
        spikeController = GameObject.Find("SpikeController").GetComponent<SpikeController>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyValuePair <int, int> t = spikeController.MoveSpike(body, pos, dirX);
        pos = t.Key;
        dirX = t.Value;
    }
}
