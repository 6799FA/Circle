using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    public float goalSpeed = 0;
    public float gravityScale = 0;

    public float maxHp = 100;
    public float minusHp = 1;
}

public class PlayerMove : MonoBehaviour {

    Transform _tr;
    Transform _trPlanet;    //현재 진입해 있는 행성의 Transform

    Rigidbody2D _rb;

    [SerializeField] Stat _stat = new Stat();

    Vector2 _velocity;
    Vector2 _angleCorrection;
    
    bool _onSurface = false;
    bool _isJump = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            UiManager.Instance.StartAnimation("GameOver");
            UiManager.Instance.StartAnimation("ExitPause");
            Destroy(gameObject);
            return;
        }

        if (!collision.CompareTag("GravityZone"))
            return;

        // TODO: 행성 정보를 받아와서 행성마다 다른 특징(스피드, 중력 가속도) 등을 구현
        Planet inPlanet = collision.GetComponentInParent<Planet>();
        
        GameManager.Instance.CreatePlanet(inPlanet.planetNum, inPlanet.planetInfor);

        _trPlanet = collision.transform;
        _tr.parent = _trPlanet;
        
        _angleCorrection = CalculateAngleCorrection(_trPlanet.position);

    }

    // 진입 각도에 따른 보정값 계산
    // 반시계방향으로 회전할 경우 보정해야 하는 값이 생긴다.(시계방향일 경우 생기지 않음)
    // 보정내용: y축으로 180도 회전, z축에 구해야 하는 값 -1 곱하고 대입
    // HACK: 보정값 없이 바로 보완되도록 만들기
    private Vector2 CalculateAngleCorrection(Vector3 center) {

        //행성에서 플레이어의 방향 벡터
        Vector3 planetToPlayerVector = (_tr.position - center).normalized;
        Vector3 playerDirectionVector = Vector3.Cross(planetToPlayerVector, Vector3.forward);

        _velocity.x = Vector3.Dot(playerDirectionVector, _rb.velocity);
        _velocity.y = Vector3.Dot(planetToPlayerVector, _rb.velocity);

        if (_velocity.x < 0) {
            _velocity.x *= -1;

            return new Vector2(180, -1);
        }
        else {
            return Vector2.up;
        }

    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Planet")) {
            _onSurface = true;

            _velocity.x = _stat.goalSpeed;
            _velocity.y = 0;

        }

    }

    // Start is called before the first frame update
    void Start() {
        _tr = gameObject.transform;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _rb.velocity = Vector2.up * _stat.goalSpeed;
        
    }

    // Update is called once per frame
    void Update() {
        if (!_trPlanet)
            return;

        if (Input.GetButtonDown("Jump")) {
            _isJump = true;
        }
        else if (Input.GetButtonUp("Jump")) {
            _angleCorrection = CalculateAngleCorrection(_trPlanet.position);
            _isJump = false;
        }


    }

    private void LateUpdate() {

        if (!_trPlanet)
            return;

        // 플레이어의 발이 행성 중심을 향하도록 하는 각 계산
        float playerAngle = Mathf.Atan2(_tr.position.y - _trPlanet.position.y, _tr.position.x - _trPlanet.position.x) * Mathf.Rad2Deg - 90;

        // 회전 방향에 따른 보정값을 넣어 최종적인 플레이어의 형태 결정
        _tr.eulerAngles = new Vector3(0, _angleCorrection.x, playerAngle * _angleCorrection.y);

        if (_isJump) {
            return;
        }

        //플레이어의 수직 이동 값
        if (!_onSurface)
            _velocity.y -= _stat.gravityScale * Time.deltaTime;

        // 플레이어 움직임
        _rb.velocity = (_tr.right * _velocity.x + _tr.up * _velocity.y);

        //플레이어의 상태 초기화
        _onSurface = false;

    }

}