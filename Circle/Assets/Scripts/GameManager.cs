using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlanetScript[] planets = null;

    [SerializeField] PercenTime2Int planetCreate = null;

    [SerializeField] Vector3 lastCreatePos = Vector3.zero;
    [SerializeField] float lastCreateSize = 0f;
    int lastCreateSign = 1;

    float time = 0;

    static int score;

    public float level;
    [SerializeField] int log;

    [SerializeField] float a = 5;
    [SerializeField] float b = 5;

    [SerializeField] int preCreate = 3;
    [SerializeField] int makingPlanetNum = 2;

    bool isPlay = true;

    static GameManager gm;

    public static GameManager GetManager() {
        if (!gm)
            gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        return gm;
    }
    
    public void CreatePlanet(int planetNum) {

        for (; makingPlanetNum < planetNum + preCreate; makingPlanetNum++) {

            //Vector3 createPos = new Vector3(24 * makingPlanetNum, Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI)) * 12, 0);

            float randomAngle = Random.Range(-0.5f, 0.5f);

            PlanetScript nextPlanet = planets[planetCreate.Time2Int(time)];

            
            lastCreateSign *= -1;
            Vector3 createPos = new Vector3(lastCreatePos.x + 80, 130 * lastCreateSign, 0);

            PlanetScript createdPlanet = Instantiate(nextPlanet, createPos, Quaternion.identity).GetComponent<PlanetScript>();

            createdPlanet.planetNum = makingPlanetNum;

            lastCreatePos = createPos;
            lastCreateSize = createdPlanet.size;

        }
        score++;
        UIScript.GetUIScript().ChangeText(0, "SCORE " + score);

        level = Mathf.Log(time + log, log);

    }

    float EnemySpeedSet(float nowScore) {
        return Mathf.Log(score + b, b) * a;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
            return;

        time += Time.deltaTime;

    }

    public void GameOver () {

        UIScript.GetUIScript().OpenPanel(1);
        isPlay = false;

    }
}
