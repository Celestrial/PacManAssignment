using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Ghost : MonoBehaviour {
        public int ghostID;
        Vector3 target = Vector3.zero;
        GameObject destination;
        GameObject origin;
        Vector3 tempPos;
        float timer = 0;
        static int releaseGhost = 0;
        GhostState ghostState;

	    // Use this for initialization
	    void Start () {
            ghostState = GhostState.WANDER;
	    }
	
	    // Update is called once per frame
	    void Update () {
	        
	    }

        void FixedUpdate()
        {
            if (releaseGhost < 4)
            {
                if (timer > 10)
                {
                    if (releaseGhost == ghostID)
                    {
                        release();
                        ++releaseGhost;
                        timer = 0;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }

            if (ghostState == GhostState.WANDER)
            {
                transform.position = transform.position + new Vector3(0, 0, 1f) * Mathf.Cos(Time.time) * .0005f;
            }
            if (ghostState == GhostState.LERPING)
            {
                transform.position = Vector3.Lerp(tempPos, destination.transform.position, .1f)*Time.deltaTime; 
            }
            if (ghostState == GhostState.NAVIGATING)
            {

            }
        }
        
        void release()
        {
            tempPos = transform.position;
            origin = destination = GameObject.Find("Plane.072");
            ghostState = GhostState.LERPING;
        }

        [RPC]
        void setTarget(int gID, Vector3 target)
        {
            this.target = target;
        }
    }
}