using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

namespace Comp476A3
{
    public class Board : MonoBehaviour
    {
        static ArrayList pellets;
        public GameObject startPos1;
        public GameObject startPos2;
        // Use this for initialization
        void Start()
        {        }

        // Update is called once per frame
        void Update()
        {        }

        public Vector3 getPlayerStartPos(int playerNo)//LOAD POSITION OF START TILES
        {
            if (playerNo == 1)
                return startPos1.transform.position;
            else
                return startPos2.transform.position;
        }
    }
}