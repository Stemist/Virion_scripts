using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MitochondriaSpawn : MonoBehaviour {

    private int rotationZ;


	// Use this for initialization
	void Start () {
        int randNum = Random.Range(1, 359);

        rotationZ = randNum;

        //transform.rotation = rotationZ;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime);
	}
}
