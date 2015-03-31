using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Player : MonoBehaviour {

        //public GameObject Avatar;

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

        #region PRIVATE FUNCTIONS
        PlayerDirection GetDirection()
        {
            return direction;
        }
        void SetNewTarget(GameObject target)
        {
            destination = target;
        }
        #endregion
	    // Use this for initialization
	    void Start () {
	
	    }
	
	    // Update is called once per frame
	    void Update () {
	
	    }
    }
}