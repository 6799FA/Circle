using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject trailObject = null;

    Transform tr;
    UIScript ui;

    Rigidbody2D rb;
    
    Transform trPlanet;    //현재 진입해 있는 행성의 Transform

    GameManager managerScript;

    //플레이어의 기본 능력
    [SerializeField] float jumpScale = 0;
    [SerializeField] float goalSpeed = 0;
    [SerializeField] float gravityScale = 0;

    [SerializeField] float maxHp = 100;
    [SerializeField] float minusHp = 1;

    float nowHp;
    float nowHpMinus;

    float moveWeight = 1;
    float vertical = 0; //플레이어의 상하 이동 조절, 점프나 중력 등
    float speed = 0;    //플레이어의 좌우 이동 조절

    //플레이어의 방향을 보정하기 위한 변수
    int yAngle = 0;
    int rightFactor = 1;

    //플레이어의 상태를 확인하기 위한 변수
    bool inGravity = false;
    bool onSurface = false;
    bool isJump = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("GravityZone"))
            return;

        PlanetScript inPlanet = collision.transform.parent.GetComponent<PlanetScript>();

        tr.parent = inPlanet.transform;
        
        inGravity = true;
        moveWeight = inPlanet.weight;
        trPlanet = collision.transform;

        if (managerScript)
        {
            managerScript.CreatePlanet(inPlanet.planetNum);
        }

        //행성에 진입할 때 속도를 자연스럽게 움직이도록 변형

        Vector3 positionVector = (tr.position - trPlanet.position).normalized;
        
        vertical = Vector3.Dot(positionVector, rb.velocity);
        speed = Vector3.Dot(Vector3.Cross(positionVector, Vector3.forward), rb.velocity);

        if (speed < 0)
        {
            yAngle = 180;
            rightFactor = -1;
            speed *= -1;
        }
        else {
            yAngle = 0;
            rightFactor = 1;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tr.parent = null;
        inGravity = false;
        trPlanet = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            onSurface = true;
            speed = goalSpeed;
            return;
        }

        vertical = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ui = GameObject.FindWithTag("UI").GetComponent<UIScript>();

        nowHp = maxHp;
        nowHpMinus = minusHp;

        managerScript = GameManager.GetManager();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0)
            isJump = true;
        else
            isJump = false;

        trailObject.transform.position = new Vector3(tr.position.x, tr.position.y, Mathf.Abs(tr.position.z) + 1);
        nowHp -= nowHpMinus * Time.deltaTime;

        ui.GaugeImage(0, nowHp, maxHp);

        if (nowHp < 0) {
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(this);
            trailObject.SetActive(false);
            managerScript.GameOver();
        }

    }

    private void FixedUpdate()
    {
        if (!inGravity)
            return;

        //플레이어의 발이 행성 중심으로 각 변형
        float playerAngle = Mathf.Atan2(tr.position.y - trPlanet.position.y, tr.position.x - trPlanet.position.x) * Mathf.Rad2Deg - 90;

        tr.eulerAngles = new Vector3(0, yAngle, playerAngle * rightFactor);
        
        //플레이어의 수직 이동 값
        if (isJump)
            vertical += jumpScale * Time.fixedDeltaTime / moveWeight;
        else if (!onSurface)
            vertical -= gravityScale * Time.fixedDeltaTime;
        else
            vertical = 0;

        rb.velocity = (tr.right * speed + tr.up * vertical) * managerScript.level ;

        //플레이어의 상태 초기화
        onSurface = false;
        
    }
    
}