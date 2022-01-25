using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    Transform tr;
    Transform trPlayer;

    public float speed;

    [SerializeField] Vector3 maxPlayerPos;

    float xPos = 0;
    public static float yPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform;
        trPlayer = GameObject.FindWithTag("Player").transform;

        xPos = tr.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trPlayer)
        {
            //yPos = trPlayer.position.y;

            if (trPlayer.position.x >= maxPlayerPos.x + tr.position.x) {
                xPos = trPlayer.position.x - maxPlayerPos.x;
            }
            else {
                xPos += Time.deltaTime * speed;
            }
        }
        else
        {
            xPos += Time.deltaTime * speed;
        }


        tr.position = new Vector3(xPos, yPos, -8);
    }
}
