using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Game : Photon.MonoBehaviour
    {
        public GameObject pacMan;
        public GameObject pacWoman;
        public GameObject gameboard;
        public static int pacManScore = 0;
        public static int pacWomanScore = 0;
        Board boardScript;
        bool isServer = true;
        public static GameState gameState = GameState.WAITING;
        //bool ready = false;
        // Use this for initialization
        void Start()
        {
            boardScript = gameboard.GetComponent<Board>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.gameState == GameState.WAITING && PhotonNetwork.countOfPlayers == 2)
                photonView.RPC("startGame", PhotonTargets.All);
            if (pacManScore + pacWomanScore == 330)
            {
                gameState = GameState.GAMEOVER;
            }
        }

        [RPC]
        void startGame()
        {
            gameState = GameState.PLAYING;
        }
    }
}
