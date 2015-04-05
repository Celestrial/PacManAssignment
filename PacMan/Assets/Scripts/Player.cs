using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Player : Photon.MonoBehaviour {

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
        PlayerDirection direction; //UP, DOWN, LEFT, RIGHT
        PlayerDirection nextDirect;//TEMP DIRECTION TO ALLOW LEAWAY WHEN MOVING
        PlayerState     playerState; //NORMAL , SPEEDUP, STOP
        const float ACCEPT_RADIUS = 0.0005f;
        const float BOOST_TIME_LIMIT = 10f;
        const float BASE_SPEED = 0.05f;
        const float BOOST_SPEED = 0.075f;
        bool speedBoost = false;
        float speedBoostTimer = 0f;
        bool invulnerable = false;
        float invulnerableTimer = 0f;
        const int INVULNERABLE_TIME = 3;
        GameObject smokeTrail;
        #endregion

        // Use this for initialization
        void Start()
        {
            pacManStartPos = GameObject.Find("Plane.163");
            pacWomanStartPos = GameObject.Find("Plane.085");
            playerState = PlayerState.WAITING;
            resetPosition();
            smokeTrail = transform.Find("SmokeTrail").gameObject;
            smokeTrail.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.gameState != GameState.GAMEOVER)
            {
                if (Game.gameState == GameState.PLAYING)
                    startGame();
                if (playerState != PlayerState.WAITING)
                {
                    if (!speedBoost)
                        smokeTrail.SetActive(false);
                    if (photonView.isMine)
                    {
                        getInput();
                        boostManager();
                    }
                }
                if (invulnerable && invulnerableTimer < INVULNERABLE_TIME)
                {
                    invulnerableTimer += Time.deltaTime;
                }
                else
                {
                    invulnerable = false;
                    invulnerableTimer = 0;
                }
            }
        }
        void FixedUpdate()
        {
            if(Game.gameState != GameState.GAMEOVER)
                if (photonView.isMine)
                {
                    if (playerState != PlayerState.WAITING && playerState != PlayerState.STOP)
                        movePlayer();
                }
        }

        public void killed()//DISPLAY EFFECTS AND SOUND, AND RESET POSITION
        {
            if (speedBoost)
                speedBoostTimer += BOOST_TIME_LIMIT;
            if (!invulnerable )
            {
                PhotonNetwork.Instantiate("DeathEffect", transform.position, Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Explosion", transform.position, Quaternion.identity, 0);
                if (photonView.isMine)
                {
                    Instantiate(Resources.Load("haha") as GameObject, Camera.main.transform.position, Quaternion.identity);
                }
                invulnerable = true;
                invulnerableTimer = 0;
                resetPosition();
            }
        }

        void resetPosition()//RESET POSITION
        {
            direction = PlayerDirection.UP;
            transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
            if (this.name == "pacManSphere(Clone)")
            {
                SetOrigin(pacManStartPos);
            }
            else
            {
                SetOrigin(pacWomanStartPos);
            }
        }

        #region PRIVATE FUNCTIONS
        [RPC]
        void startGame()//CHANGE STATE TO START GAME
        {
            playerState = PlayerState.NORMAL;
        }
        public  void playEatSound(int soundID)
        {
            if (photonView.isMine)
            {
                if(soundID == 1)
                    PhotonNetwork.Instantiate("Eat_Sound_Effect", transform.position, Quaternion.identity, 0);
                else if(soundID == 2)
                    PhotonNetwork.Instantiate("Pacman_Eating_Special", transform.position, Quaternion.identity, 0);
            }
        }
        public  void speedUp()//SPEED UP AND CREATE SMOKE TRAIL
        {
            transform.Find("SmokeTrail").gameObject.SetActive(true);
            speedBoost = true;
        }
        private void boostManager()//MANAGE BOOST DURATION ETC
        {
            if (speedBoost)
            {
                speedBoostTimer += Time.deltaTime;
                if (speedBoostTimer >= BOOST_TIME_LIMIT)
                {
                    speedBoost = false;
                    speedBoostTimer = 0;
                    transform.Find("SmokeTrail").gameObject.SetActive(false);
                }
            }
        }
        private void getInput()//HANDLE USER INPUT
        {
            if (Input.GetKey(KeyCode.W))
            {   
                if(inputValid(0))
                    nextDirect = PlayerDirection.UP;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (inputValid(1))
                    nextDirect = PlayerDirection.RIGHT;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (inputValid(2))
                    nextDirect = PlayerDirection.DOWN;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (inputValid(3))
                 nextDirect = PlayerDirection.LEFT;
            }
            restoreMovement();
        }
        bool inputValid(int i)
        {
            return destination.GetComponent<Pellet>().neighbours[i] != null;
        }
        private PlayerDirection GetDirection()
        {
            return direction;
        }
        private void movePlayer()//MOVE PLAYER IN CURRENT DIRECTION, MODE ACCEPT RADIUS BASED ON SPEED
        {
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
        private void changeDestination()//SWITCH DIRECTION STATE
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
                        transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
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
                        transform.rotation = Quaternion.LookRotation(-Vector3.right, Vector3.forward);
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
                        transform.rotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
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
                        transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.forward);
                    }
                    break;
            }
        }
        private void SetNewTarget(GameObject target)
        {
            destination = target;
        }
        public  void SetOrigin(GameObject org)//UPDATE ORIGIN TILE
        {
            transform.position = org.transform.position;
            Origin = org;
            switch (direction)
            {
                case PlayerDirection.UP:
                    destination = Origin.GetComponent<Pellet>().neighbours[0];
                    break;

                case PlayerDirection.RIGHT:
                    destination = Origin.GetComponent<Pellet>().neighbours[1];
                    break;

                case PlayerDirection.DOWN:
                    destination = Origin.GetComponent<Pellet>().neighbours[2];
                    break;

                case PlayerDirection.LEFT:
                    destination = Origin.GetComponent<Pellet>().neighbours[3];
                    break;

                //destination = Origin.GetComponent<Pellet>().neighbours[0];
            }
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