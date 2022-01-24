using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [SerializeField] float rotateSpeed = 0;

	void Update () {

        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

	}

}
