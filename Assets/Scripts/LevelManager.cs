using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    
    //Variable hold the amount of time it takes the player to respawn after dying
    public float waitToRespawn;

    public int bloodVialsCollected;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }


    private IEnumerator RespawnCo()
    {
        //Hides player
        PlayerController.instance.gameObject.SetActive(false);

        //Time player takes to respawn
        yield return new WaitForSeconds(waitToRespawn);
        //Player is once again visible
        PlayerController.instance.gameObject.SetActive(true);
        //Player position is set to checkpoint when respawning
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay(); 
    }
}
