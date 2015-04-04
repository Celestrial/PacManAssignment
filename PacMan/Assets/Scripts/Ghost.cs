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
        float pingTimer = 0;
        int releaseGhost = 0;
        const int TIME_TO_RELEASE = 8;
        const int PELLET_LAYER = 8;
        const float ACCEPT_RADIUS = 0.0005f;
        const float BASE_SPEED = 0.04f;
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
                    ghostState = GhostState.NAVIGATING;

                }
                if (ghostState == GhostState.NAVIGATING)
                {
                    if (pingTimer == 0)//ping player location and update navigation info
                    {
                        pingPlayer();
                        pingTimer += Time.deltaTime;
                    }
                    else if (pingTimer < 10)
                    {
                        pingTimer += Time.deltaTime;
                    }
                    else
                    {
                        pingTimer = 0;
                    }
                    move();
                }
            }
        }
        void pingPlayer()
        {
            Vector3 player = GameObject.Find("pacManSphere(Clone)").transform.position;
            Collider[] hits = Physics.OverlapSphere(player, Random.Range(.05f, .15f),1 << PELLET_LAYER);
            //foreach (Collider pellet in hits)
            //{
                int destPellet = Random.Range(0, hits.Length);
                //if (pellet.tag != "Pellet")
                //{
                //    Debug.LogError("Not a Pellet: " + pellet.name);
                //}
                updateTarget(ref hits[destPellet]);
            //}
        }
        void updateTarget(ref Collider target)
        {
            this.target = target.transform.position;
        }
        void move()
        {
            Debug.DrawLine(transform.position, target, Color.magenta);
            if ((transform.position - destination.transform.position).magnitude < ACCEPT_RADIUS)
            {
                transform.position = destination.transform.position;
            }
            if (transform.position == target)
            {
                pingPlayer();
            }
            if (transform.position == destination.transform.position)
            {
                updateTiles();
            }
            else
            {
                Debug.DrawLine(origin.transform.position, destination.transform.position, Color.green);
                transform.position += (destination.transform.position - transform.position).normalized *
                       BASE_SPEED * Time.deltaTime;
            }
        }
        void updateTiles()//DECIDE WHICH PATH TO TAKE TO GET TO TARGET
        {
            GameObject tempOrigin = destination;
            destination = getBestTile();
            origin = tempOrigin;
        }
        GameObject getBestTile()//RETURN BEST TILE TO GET TO TARGET AND NOT GO BACKWARDS
        {
            if (destination.tag == "Portal")
                return destination;
            GameObject[] options = destination.GetComponent<Pellet>().neighbours;
            int shortestTile = -1;
            float shortestLength = 5;
            Debug.Log(options);
            for (int i = 0; i < options.Length; ++i)
            {
                if (options[i] != null && options[i] != origin)//PREVENT MOVING BACKWARDS
                {
                    //GET TILE WITH SHORTEST PATH TO TARGET
                    float tempLengh = (options[i].transform.position - target).magnitude;
                    if (tempLengh < shortestLength)
                    {
                        shortestTile = i;
                        shortestLength = tempLengh;
                    }
                }
            }
            return options[shortestTile];
        }

        public void setOrigin(GameObject exit)
        {
            transform.position = exit.transform.position;
            origin = exit;
            destination = exit;
            destination = getBestTile();
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