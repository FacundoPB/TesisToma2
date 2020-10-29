using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rbPlayer;
    public Camera cam;
    Vector2 movement;
    Vector2 worldPos;
    public int maxhealthPlayer = 200;
    public int currentHealth;
    public GameObject deathEffect;
    //public HealthBar healthBar;


    // Update is called once per frame
    private void Start()
    {
        /*currentHealth = maxhealthPlayer;
        healthBar.SetMaxHealth(maxhealthPlayer);*/
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        //mousePos = cam.ScreenToViewportPoint(Input.mousePosition);



        //Debug.Log(worldPos);
        //Debug.Log(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rbPlayer.MovePosition(rbPlayer.position + movement * moveSpeed * Time.fixedDeltaTime);
        /*
        Vector2 lookDir = worldPos - rbPlayer.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x)*Mathf.Rad2Deg-90f;
        rbPlayer.rotation = angle;
        
        //transform.rotation = Quaternion.LookRotation(lookDir);
        
        */

    }
   /* public void PlayerTakeDamage(int enemyDamage)
    {
        currentHealth -= enemyDamage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, 4f);
    }*/
}
