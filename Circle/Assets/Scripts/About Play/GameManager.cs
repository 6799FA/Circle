using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] Planet[] _planets = null;

    [SerializeField] float _xCreateRange;

    [SerializeField] Vector3 _lastCreatePos = Vector3.zero;
    [SerializeField] float _lastCreateSize = 0f;

    [SerializeField] int preCreate = 3;
    [SerializeField] int makingPlanetNum = 2;

    bool isPlay = true;

    public static GameManager Instance { get; private set; }
    

    public void CreatePlanet(int planetNum, PlanetInfor infor) {

        for (; makingPlanetNum < planetNum + preCreate; makingPlanetNum++) {

            Planet planet = _planets[Random.Range(0, _planets.Length)];

            float createXPos = Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI)) *_xCreateRange;
            float createYPos = _lastCreatePos.y + (_lastCreateSize + planet.planetInfor.size) * 0.5f + 0.5f;

            Vector3 createPos = new Vector3(createXPos , createYPos, 0);
            
            Planet createdPlanet = Instantiate(planet, createPos, Quaternion.identity).GetComponent<Planet>();
            createdPlanet.planetNum = makingPlanetNum;

            _lastCreatePos = createPos;
            _lastCreateSize = planet.planetInfor.size;

        }

    }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
    }

    // Update is called once per frame
    void Update() {
        if (!isPlay)
            return;

    }

    public void GameOver() {
        isPlay = false;

    }
}
