using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetInfor {
    public float size;
    public float gravityScale;
    public float moveScale;

}

public class Planet : MonoBehaviour {

    [SerializeField] int _planetNum = 0;
    public int planetNum {
        get {
            return _planetNum;
        }
        set {
            _planetNum = value;
        }
    }

    protected Vector3 originPos;

    [SerializeField] PlanetInfor _planetInfor = null;
    public PlanetInfor planetInfor { get { return _planetInfor; } }

    float _beingTime = 0;

    // Start is called before the first frame update
    protected virtual void Start() {

        Color color = Random.ColorHSV(0f, 1f, 0.9f, 1f, 0.4f, 0.6f) + new Color(0f, 0f, 0f, -1);
        //color = Random.ColorHSV() * 0.5f + new Color(0.3f, 0.3f, 0.3f, -1);
        gameObject.GetComponentInChildren<SpriteRenderer>().color -= color;

        _beingTime = Random.Range(0, Mathf.PI);

        originPos = transform.localPosition;
    }
    
    private void Update() {
        _beingTime += Time.deltaTime;

        transform.localPosition = originPos + Vector3.up * Mathf.Sin(_beingTime) * planetInfor.moveScale;

    }

}
