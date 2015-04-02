using UnityEngine;
using System.Collections;

public class autoDestruct : MonoBehaviour {
    float destroyTimer = 0;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (destroyTimer > 1)
            Destroy(gameObject);
        else
            destroyTimer += Time.deltaTime;
	}
}
