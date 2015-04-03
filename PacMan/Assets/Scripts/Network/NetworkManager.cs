using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class NetworkManager : Photon.MonoBehaviour
    {
        private const string VERSION = "1.0.0";
        public string roomName = "buffet";
        public string pacManPrefab = "pacMan";
        public string pacWomanPrefab = "pacWoman";
        public GameObject gameboard;
        public int playerCount = 0;
        Board boardScript;
        // Use this for initialization
        void Start()
        {
            boardScript = gameboard.GetComponent<Board>();
            PhotonNetwork.ConnectUsingSettings(VERSION);
        }

        void OnJoinedLobby()
        {
            RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        void OnJoinedRoom()
        {
            GameObject tempPacMan;
            GameObject tempPacWoman;
            playerCount = PhotonNetwork.countOfPlayers;
            if (playerCount == 1)
            {
                //tempPacMan = (GameObject)PhotonNetwork.Instantiate("PacManPC", boardScript.startPos1.transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward), 0);
                tempPacMan = (GameObject)PhotonNetwork.Instantiate("pacManSphere", boardScript.startPos1.transform.position, Quaternion.LookRotation(Vector3.up, Vector3.forward), 0);

                ++playerCount;
            }
            else
            {
                //tempPacWoman = (GameObject)PhotonNetwork.Instantiate("PacWomanPC", boardScript.startPos2.transform.position, Quaternion.LookRotation(Vector3.up), 0);
                tempPacWoman = (GameObject)PhotonNetwork.Instantiate("pacWomanSphere", boardScript.startPos2.transform.position, Quaternion.LookRotation(Vector3.up), 0);
            }
        }
    }
}