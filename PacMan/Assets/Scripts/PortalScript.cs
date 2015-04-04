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
            transform.FindChild("PortalEffect").gameObject.SetActive(true);
            transform.FindChild("PortalEffect").GetComponent<PortalEffectScript>().exit.gameObject.SetActive(true);
            //if (transform.name != "PortalEffect1a" && transform.name != "PortalEffect1b")
            //{
            //    transform.parent.transform.FindChild("PortalEffect1a").gameObject.SetActive(true);
            //    transform.parent.transform.FindChild("PortalEffect1b").gameObject.SetActive(true);
            //}
            //else
            //{
            //    transform.parent.transform.FindChild("PortalEffect2a").gameObject.SetActive(true);
            //    transform.parent.transform.FindChild("PortalEffect2b").gameObject.SetActive(true);
            //}

            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<Player>().SetOrigin(exit);
            else
                    other.gameObject.GetComponent<Ghost>().setOrigin(exit);
        }
    }
}