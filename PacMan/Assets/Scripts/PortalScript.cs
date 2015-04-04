using UnityEngine;
using System.Collections;

namespace Comp476A3
{
    public class PortalScript : MonoBehaviour
    {
        public GameObject exit;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<Player>().SetOrigin(exit);
            else
                other.gameObject.GetComponent<Ghost>().setOrigin(exit);
        }
    }
}