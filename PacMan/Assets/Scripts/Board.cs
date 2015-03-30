using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Board : MonoBehaviour {
    static ArrayList pellets;
    public GameObject startPos1;
    public GameObject startPos2;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 getPlayerStartPos(int playerNo)
    {
        if (playerNo == 1)
            return startPos1.transform.position;
        else
            return startPos2.transform.position;
    }
}
