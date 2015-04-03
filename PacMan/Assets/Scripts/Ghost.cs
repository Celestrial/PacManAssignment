using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Ghost : Photon.MonoBehaviour
    {
        public int ghostID;
        Vector3 target = Vector3.zero;
        GameObject destination;
        GameObject origin;
        Vector3 tempPos;
        float timer = 0;
        int releaseGhost = 0;
        const int TIME_TO_RELEASE = 8;
        bool gameStarted = false;
        GhostState ghostState;

        // Use this for initialization
        void Start()
        {
            ghostState = GhostState.WAITING;
        }

        // Update is called once per frame
        void Update()
        {
            if (!gameStarted && PhotonNetwork.countOfPlayers == 2)
            {
                photonView.RPC("startGame", PhotonTargets.All);
            }
        }

        void FixedUpdate()
        {
            if (ghostState != GhostState.WAITING)
            {
                if (releaseGhost < 4)
                {
                    if (timer > TIME_TO_RELEASE * (1 + ghostID))
                    {

                        release();
                        ++releaseGhost;
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
                    //transform.position = Vector3.Lerp(tempPos, destination.transform.position, .0001f)*Time.deltaTime; 
                    transform.position = destination.transform.position;

                }
                if (ghostState == GhostState.NAVIGATING)
                {

                }
            }
        }

        [RPC]
        void startGame()
        {
            ghostState = GhostState.WANDER;
            gameStarted = true;
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