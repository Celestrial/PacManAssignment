using UnityEngine;
using System.Collections;


public class PortalEffectScript : MonoBehaviour {
    public GameObject exit;
    float timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.active == true)
        {
            if (timer > 1.5f)
                gameObject.SetActive(false);
            else
                timer += Time.deltaTime;
        }
	}
}
