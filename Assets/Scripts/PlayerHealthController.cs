using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public GameObject deathEffect;

    /**
     * Amount of time player is invincible for
     */
    public float invincibleLength;

    /**
     * Countdown until invicibility is removed
     */
    private float invincibleCounter;

    private SpriteRenderer theSR;

    //Called just before start
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Player spawns with full health
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(invincibleCounter > 0)
        {
            //Counter reduced by 1 every second or by 1/60th of a second per frame
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                //Player sprite opacity changes to give feedback to player to show invincibility effect has worn off
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {


            //Current health is reduced by 1
            currentHealth--;


            //If player loses all health, player disappears
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                //set invincible counter to equal invincibility time
                invincibleCounter = invincibleLength;
                //Change alpha value to give player feedback of damage
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                
                //Runs method from other script
                PlayerController.instance.Knockback();
            }

            //Updates UI showing health display
            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
            if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
