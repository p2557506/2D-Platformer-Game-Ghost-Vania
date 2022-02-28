using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool isBloodVial, isHeal;

    private bool isCollected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If statement to check that player is interacting with vial the  for the first and only time
        if (other.CompareTag("Player") && !isCollected)
        {

            if (isBloodVial)
            {
                //Counts number of blood vials collected
                LevelManager.instance.bloodVialsCollected++;
                isCollected = true;

                //Removes blood vial
                Destroy(gameObject);

                UIController.instance.UpdateBloodVialCount();
            }

            if (isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);
                }
            }

        }

    }
}
