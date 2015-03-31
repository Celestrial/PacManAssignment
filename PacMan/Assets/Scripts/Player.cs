using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Player : MonoBehaviour {

        //public GameObject Avatar;

        #region PUBLIC VARIABLES
        public GameObject pacManStartPos;   
        public GameObject pacWomanStartPos; 
        #endregion

        #region PRIVATE VARIABLES
        GameObject destination;//destination tile 
        GameObject Origin;//tile Player is comming from
        Vector3 Position;//current player position
        PlayerDirection direction; //UP, DOWN, LEFT, RIGHT
        PlayerState playerState; //NORMAL , SPEEDUP, STOP
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
            playerState = PlayerState.NORMAL;
            direction = PlayerDirection.UP;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region PRIVATE FUNCTIONS
        PlayerDirection GetDirection()
        {
            return direction;
        }
        void SetNewTarget(GameObject target)
        {
            destination = target;
        }
        public void SetOrigin(GameObject org)
        {
            Origin = org;
        }
        #endregion

    }
}