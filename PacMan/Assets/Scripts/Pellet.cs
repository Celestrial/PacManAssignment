using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Pellet : Photon.MonoBehaviour
    {
        public const float RAY_LENGTH = 0.02f;
        public GameObject[] neighbours = new GameObject[4]; //ORDER IS: UP, RIGHT, DOWN, LEFT
        public bool Special = false;
        public bool eaten = false;
        float timer = 0;

        // Use this for initialization
        void Start()
        {
            getNeighbours();
        }

        // Update is called once per frame
        void Update()
        {
            //DebugLines();

        }

        void OnTriggerEnter(Collider other)
        {
            if (Special && !eaten)
            {
                other.GetComponent<Player>().speedUp();
                other.GetComponent<Player>().playEatSound(2);
                if (other.name == "PacManPC(Clone)")
                    Game.pacManScore += 5;
                else
                    Game.pacWomanScore += 5;
            }
            else if (!eaten)
            {
                other.GetComponent<Player>().playEatSound(1);
                if (other.name == "PacManPC(Clone)")
                    Game.pacManScore += 1;
                else
                    Game.pacWomanScore += 1;
            }
            eaten = true;
            transform.renderer.enabled = false;
        }

        private void DebugLines()
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * RAY_LENGTH, Color.magenta);
            Debug.DrawLine(transform.position, transform.position + transform.right * RAY_LENGTH, Color.blue);
            Debug.DrawLine(transform.position, transform.position - transform.up * RAY_LENGTH, Color.green);
            Debug.DrawLine(transform.position, transform.position - transform.right * RAY_LENGTH, Color.red);
        }
        void FixedUpdate()
        {
            colorFlashSpeed();
        }
        private void colorFlashSpeed()
        {
            if (Special && timer > .1f)
            {
                colorFlash();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        void colorFlash()
        {
            Vector3 color = new Vector3(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
            transform.renderer.material.color = new Color(color.x, color.y, color.z);
        }
        void getNeighbours()
        {
            RaycastHit hit;
            Physics.Linecast(transform.position, transform.position + transform.up * RAY_LENGTH, out hit);
            if (hit.collider != null)
                neighbours[0] = hit.collider.gameObject;
            Physics.Linecast(transform.position, transform.position + transform.right * RAY_LENGTH, out hit);
            if (hit.collider != null)
                neighbours[1] = hit.collider.gameObject;
            Physics.Linecast(transform.position, transform.position - transform.up * RAY_LENGTH, out hit);
            if (hit.collider != null)
                neighbours[2] = hit.collider.gameObject;
            Physics.Linecast(transform.position, transform.position - transform.right * RAY_LENGTH, out hit);
            if (hit.collider != null)
                neighbours[3] = hit.collider.gameObject;
        }
    }
}