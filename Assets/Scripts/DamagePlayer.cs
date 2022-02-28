using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Looking for object that enters a trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {



            //Debug.Log("Hit"); //To check collision

            //FindObjectOfType<PlayerHealthController>().DealDamage(); Less effecient
            PlayerHealthController.instance.DealDamage();
        }
    }

}
