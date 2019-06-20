using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Rotate the background image slowly to the feeling of movemening in the liquid medium.
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime);
    }
}
