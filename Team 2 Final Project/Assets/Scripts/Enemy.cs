using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealthBar healthBar;

    public float health = 100;

    public float clickDamage = 10f;

    public float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        
        if(healthBar == null)
        {
            Debug.LogError("Health bar script not found, blin!");
            return;
        }
        healthBar.SetMaxValue(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.SetValue(health);

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlotManager plantHealth = collision.gameObject.GetComponent<PlotManager>();

        if (plantHealth == null)
        {
            Debug.LogError("Plant Health script is not found");
            return;
        }
        plantHealth.PlantTakeDamage(damage);
    }

    private void OnMouseDown()
    {
        TakeDamage(clickDamage);
    }
}
