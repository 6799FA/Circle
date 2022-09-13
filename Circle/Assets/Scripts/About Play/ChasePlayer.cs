using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    Transform _tr;
    Transform _trPlayer;

    [SerializeField] float _maxPlayerYPos = 0f;
    [SerializeField] float _moveSpeed = 0f;
    [SerializeField] float _zPos = -10f;

    // Use this for initialization
    void Start() {

        _tr = gameObject.transform;
        _trPlayer = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate() {

        float yPos = _tr.position.y;

        if (_trPlayer) {
            float playerYPos = _trPlayer.position.y;

            if (playerYPos >= _maxPlayerYPos + yPos) {
                yPos = playerYPos - _maxPlayerYPos;

            }
            else {
                yPos += _moveSpeed * Time.deltaTime;
            }

        }

        _tr.position = new Vector3(0, yPos, _zPos);

    }
}
