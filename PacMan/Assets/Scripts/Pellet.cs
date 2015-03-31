﻿using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class Pellet : MonoBehaviour
    {
        public const float RAY_LENGTH = 0.02f;
        GameObject[] neighbours = new GameObject[4]; //ORDER IS: UP, RIGHT, DOWN, LEFT
        public bool Special = false;
        float timer = 0;
        // Use this for initialization
        void Start()
        {
            getNeighbours();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.DrawLine(transform.position, transform.position + transform.up * RAY_LENGTH, Color.magenta);
            //Debug.DrawLine(transform.position, transform.position + transform.right * RAY_LENGTH, Color.blue);
            //Debug.DrawLine(transform.position, transform.position - transform.up * RAY_LENGTH, Color.green);
            //Debug.DrawLine(transform.position, transform.position - transform.right * RAY_LENGTH, Color.red);

        }

        void FixedUpdate()
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