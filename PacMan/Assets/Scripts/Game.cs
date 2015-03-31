using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Game : MonoBehaviour
    {
        public GameObject pacMan;
        public GameObject pacWoman;
        public GameObject gameboard;
        public static int pacManScore = 0;
        public static int pacWomanScore = 0;
        Board boardScript;
        bool isServer = true;
        // Use this for initialization
        void Start()
        {
            boardScript = gameboard.GetComponent<Board>();
            GameObject tempPacMan = (GameObject)Instantiate(pacMan, boardScript.startPos1.transform.position, Quaternion.LookRotation(Vector3.up));
            GameObject tempPacWoman = (GameObject)Instantiate(pacWoman, boardScript.startPos2.transform.position, Quaternion.LookRotation(Vector3.up));
            //if (isServer)
            //    tempPacMan.GetComponent<Player>().setAvatar(pacMan);
            //else
            //    tempPacMan.GetComponent<Player>().setAvatar(pacWoman);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
