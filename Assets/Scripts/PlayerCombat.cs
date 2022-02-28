using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    //Damage the player deals
    public int attackDamage = 50;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Adds delay to attack to stop spamming and making the player over powered
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                Attack();
                nextAttackTime = Time.time + 1f / attackRate;//2 times per second
            }
        }
    }

    void Attack()
    {
        //Play attack animation
        anim.SetTrigger("attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Apply damage  to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit" + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    //Tool used to adjust attackpoint until satisfied

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
