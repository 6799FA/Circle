using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{

    protected Color color;

    protected Vector3 originPos;

    public int planetNum = 0;
    public float size;
    public float weight;

    [SerializeField] float movement = 0;
    [SerializeField] float beingTime = 0;

    // Start is called before the first frame update
    protected virtual void Start() {
        
        
        color = Random.ColorHSV(0f, 1f, 0.9f, 1f, 0.4f, 0.6f) + new Color(0f, 0f, 0f, -1);
        //color = Random.ColorHSV() * 0.5f + new Color(0.3f, 0.3f, 0.3f, -1);
        gameObject.GetComponent<SpriteRenderer>().color -= color;

        float beingTime = Random.Range(0, Mathf.PI);
        
        originPos = transform.localPosition;
    }

    private void Update()
    {
        beingTime += Time.deltaTime;

        transform.localPosition = originPos + Vector3.up * Mathf.Sin(beingTime) * movement;
        
    }

}
