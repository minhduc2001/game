using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyModel enemyModel;

    // Start is called before the first frame update
    void Start()
    {
        enemyModel = GameObject.Find("EnemyModel").GetComponent<EnemyModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public KeyValuePair <int, int> MoveEnemy(Rigidbody2D body, Animator anim, int pos, int dirX){
        if(pos == enemyModel.limitRight){
            dirX = -1;
            anim.SetInteger("dirX", dirX);
        }
        if(pos == enemyModel.limitLeft){
            dirX = 1;
            anim.SetInteger("dirX", dirX);
        }
        body.velocity = new Vector2(enemyModel.speed * dirX, body.velocity.y);
        pos += dirX;
        return new KeyValuePair <int, int>(pos, dirX);
    }
}
