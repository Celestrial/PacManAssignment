using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	public GameObject tileOrig;
	const float offset = 0.5f;
	const int xRange = 28;
	const int yRange = 31;
	List<GameObject> tiles;

	// Use this for initialization
	void Start () {
		tiles = new List<GameObject>();

		for(int i = 0; i < xRange; ++i)
		{
			for(int j = 0; j < yRange; ++j)
			{
				tiles.Add((GameObject)Instantiate(tileOrig,new Vector3(i+offset, 0, j+offset),Quaternion.identity));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
