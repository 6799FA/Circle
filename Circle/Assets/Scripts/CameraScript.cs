using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform tr;
    Transform trPlayer;

    [SerializeField] Vector3 maxPlayerPos = Vector3.zero;
    [SerializeField] Transform trBackground = null;

    // Use this for initialization
    void Start() {

        tr = gameObject.transform;
        trPlayer = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate() {

        float xPos;

        if (trPlayer) {
            if (trPlayer.position.x >= maxPlayerPos.x + tr.position.x) {
                xPos = trPlayer.position.x - maxPlayerPos.x;
                goto constXPos;
            }
        }

        xPos = tr.position.x;

        constXPos:

        tr.position = new Vector3(xPos, 0, -10);
        trBackground.Rotate(Vector3.up * Time.deltaTime);

    }
}
