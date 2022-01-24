using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWithItemScript : PlanetScript
{

    [SerializeField] GameObject[] ring = null;
    [SerializeField] ItemScript childItem = null;

    public float speedChange = 1;
    public float weightChange = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            childItem.ItemActive(collision.gameObject.GetComponent<PlayerScript>());
        }

    }

    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();

        if (Random.Range(0, 10) < 7) {

            ring[0].transform.localScale *= 0;
            ring[1].transform.localScale *= 0;

        }
        else {

            ring[0].GetComponent<SpriteRenderer>().color -= color * 0.5f;
            ring[1].GetComponent<SpriteRenderer>().color -= color * 0.5f;

        }
    }
    
}
