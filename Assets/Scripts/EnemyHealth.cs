using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
    public static EnemyHealth instance;

    public int maxHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    
    
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;


        //Enemy hurt animation
        
        if(currentHealth <= 0)
        {
            //Enemey dies
            Die();
        }
    }

    void Die()
    {
        //Play die animation
        anim.SetBool("isDead", true);

        //Disable enemy sprite

        GetComponent<Collider2D>().enabled = false;
        EnemyController.instance.gameObject.SetActive(false);

        //Disables collider

        Debug.Log("EnemyDied");

    }
}
