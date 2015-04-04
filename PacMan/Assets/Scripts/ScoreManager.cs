using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Comp476A3
{
    public class ScoreManager : MonoBehaviour
    {
        Text text;
        public static int pacmanScore = 0;
        public static int pacwomanScore = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update() {
            text = GetComponent<Text>();
            if (transform.name == "PacManScore")
                text.text = ""+Game.pacManScore;
            if (transform.name == "PacWomanScore")
                text.text = ""+Game.pacWomanScore;
	    }
    }
}