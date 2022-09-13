using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfor {

    public Sprite itemSpr = null;
    public float speedFactor = 1;
    public float weightFactor = 1;

}

public class ItemScript : MonoBehaviour {

    int i;

    [SerializeField] ItemInfor[] itemInfors = null;

    private void Start() {
        i = Random.Range(0, itemInfors.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = itemInfors[i].itemSpr;
    }

    public void ItemActive (PlayerMove ps) {
        
       
    }

}
