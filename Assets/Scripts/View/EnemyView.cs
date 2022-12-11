using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private EnemyController enemyController;
    private Rigidbody2D body;
    private Animator anim;

    private int pos = 0, dirX = -1;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyValuePair <int, int> t = enemyController.MoveEnemy(body, anim, pos, dirX);
        pos = t.Key;
        dirX = t.Value;
    }
}
