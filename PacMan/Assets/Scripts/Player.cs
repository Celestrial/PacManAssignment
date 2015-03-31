﻿using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Player : MonoBehaviour {

        GameObject Avatar;

        #region PUBLIC VARIABLES
        public GameObject pacManStartPos;   
        public GameObject pacWomanStartPos;
        public GameObject pacManAvatar;
        public GameObject pacWomanAvatar;
        #endregion

        #region PRIVATE VARIABLES
        GameObject      destination;//destination tile 
        GameObject      Origin;//tile Player is comming from
        Vector3         Position;//current player position
        PlayerDirection direction; //UP, DOWN, LEFT, RIGHT
        PlayerDirection nextDirect;
        PlayerState     playerState; //NORMAL , SPEEDUP, STOP
        const float ACCEPT_RADIUS = 0.0005f;
        const float BOOST_TIME_LIMIT = 10f;
        const float BASE_SPEED = 0.05f;
        const float BOOST_SPEED = 0.075f;
        bool speedBoost = false;
        float speedBoostTimer = 0f;
        #endregion

        #region Network Variables
        Vector3 syncPosition;
        int syncDirection; //1= up, 2 = right, 3 = down, 4 = left 
        #endregion

        // Use this for initialization
        void Start()
        {
            pacManStartPos = GameObject.Find("Plane.163");
            pacWomanStartPos = GameObject.Find("Plane.085");
            if (this.name == "PacManPC(Clone)")
            {
                SetOrigin(pacManStartPos);
            }
            else
            {
                SetOrigin(pacWomanStartPos);
            }
            playerState = PlayerState.NORMAL;
            direction = PlayerDirection.UP;
        }

        // Update is called once per frame
        void Update()
        {
            getInput();
            boostManager();

        }
        void FixedUpdate()
        {
            if (playerState != PlayerState.STOP)
                movePlayer();
        }

        public void speedUp()
        {
            speedBoost = true;
        }

        #region PRIVATE FUNCTIONS
        private void boostManager()
        {
            if (speedBoost)
            {
                speedBoostTimer += Time.deltaTime;
                if (speedBoostTimer >= BOOST_TIME_LIMIT)
                {
                    speedBoost = false;
                    speedBoostTimer = 0;
                }
            }
        }
        private void getInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                nextDirect = PlayerDirection.UP;
            }
            if (Input.GetKey(KeyCode.D))
            {
                nextDirect = PlayerDirection.RIGHT;
            }
            if (Input.GetKey(KeyCode.S))
            {
                nextDirect = PlayerDirection.DOWN;
            }
            if (Input.GetKey(KeyCode.A))
            {
                nextDirect = PlayerDirection.LEFT;
            }
            restoreMovement();
        }
        private PlayerDirection GetDirection()
        {
            return direction;
        }
        private void movePlayer()
        {
            //if(destination != null)//DEBUG LINE


            if (transform.position == destination.transform.position)
            {
                direction = nextDirect;
                changeDestination();
            }
            else
            {
                Debug.DrawLine(Origin.transform.position, destination.transform.position, Color.cyan);
                if ((transform.position - destination.transform.position).magnitude <= (speedBoost ? ACCEPT_RADIUS * 2 : ACCEPT_RADIUS))
                {
                    transform.position = destination.transform.position;
                }
                else
                    transform.position += (destination.transform.position - transform.position).normalized * 
                        (speedBoost ? BOOST_SPEED : BASE_SPEED) * Time.deltaTime;
            }
        }
        private void changeDestination()
        {
            GameObject temp;
            switch (direction)
            {
                case PlayerDirection.UP:
                    Origin = destination;
                    temp = destination.GetComponent<Pellet>().neighbours[0];
                    if (temp == null)
                    {
                        playerState = PlayerState.STOP;
                    }
                    else
                    {
                        destination = temp;
                        //transform.rotation = Quaternion.LookRotation(Vector3.up);
                    }
                    break;

                case PlayerDirection.RIGHT:
                    Origin = destination;
                    temp = destination.GetComponent<Pellet>().neighbours[1];
                    if (temp == null)
                    {
                        playerState = PlayerState.STOP;
                    }
                    else
                    {
                        destination = temp;
                       // transform.rotation = Quaternion.LookRotation(Vector3.right);
                    }
                    break;

                case PlayerDirection.DOWN:
                    Origin = destination;
                    temp = destination.GetComponent<Pellet>().neighbours[2];
                    if (temp == null)
                    {
                        playerState = PlayerState.STOP;
                    }
                    else
                    {
                        destination = temp;
                        //transform.LookAt(-Vector3.up);
                    }
                    break;

                case PlayerDirection.LEFT:
                    Origin = destination;
                    temp = destination.GetComponent<Pellet>().neighbours[3];
                    if (temp == null)
                    {
                        playerState = PlayerState.STOP;
                    }
                    else
                    {
                        destination = temp;
                        //transform.LookAt(-Vector3.right);
                    }
                    break;
            }
        }
        private void SetNewTarget(GameObject target)
        {
            destination = target;
        }
        private void SetOrigin(GameObject org)
        {
            transform.position = org.transform.position;
            Origin = org;
            destination = Origin.GetComponent<Pellet>().neighbours[0];
        }
        private void restoreMovement()
        {
            if (!speedBoost)
                playerState = PlayerState.NORMAL;
            else
                playerState = PlayerState.SPEEDUP;
        }
        #endregion
    }
}