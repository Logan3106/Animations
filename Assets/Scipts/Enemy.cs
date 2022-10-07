using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;
    private Animator anim;
    public GameObject spear;
    [SerializeField] float currentHealth, maxHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (currentHealth <= 0)
            {
                anim.SetTrigger("Death");
                Destroy(gameObject);
            }
        }
        void Die()
        {
            anim.SetBool("Die", true);
            Destroy(gameObject);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {


        currentHealth -= damageAmount;
        anim.SetTrigger("Hurt");
        print("enemy hit. Damage=" + currentHealth);

    }
}
