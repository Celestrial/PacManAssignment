using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Game : MonoBehaviour
    {
        public GameObject pacMan;
        public GameObject pacWoman;
        public GameObject gameboard;
        Board boardScript;
        // Use this for initialization
        void Start()
        {
            boardScript = gameboard.GetComponent<Board>();
            Instantiate(pacMan, boardScript.startPos1.transform.position, Quaternion.LookRotation(Vector3.up));
            Instantiate(pacWoman, boardScript.startPos2.transform.position, Quaternion.LookRotation(Vector3.up));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
