/* Adam Krenek
 * Final Game Project
 * This script manages the enemy's stats
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GardenManager gm;

    public EnemyHealthBar healthBar;

    public float health = 100;

    public float clickDamage = 10f;

    public float damage = 5f;

    public int bountyForKill = 2;

    private EnemyAnimalChase chase;

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
        gm = FindObjectOfType<GardenManager>();

        if(gm == null)
        {
            Debug.LogError("Garden Manager not found!");
        }
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
        gm.Transaction(bountyForKill);
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlotManager plotManager = collision.gameObject.GetComponentInParent<PlotManager>();

        if (plotManager != null && plotManager.isPlanted)
        {
            plotManager.PlantTakeDamage(damage, chase);
        }
       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlotManager plotManager = collision.gameObject.GetComponentInParent<PlotManager>();
        if (plotManager != null && plotManager.isPlanted)
        {
            plotManager.PlantTakeDamage(damage * Time.deltaTime, chase);
        }
    }
    private void OnMouseDown()
    {
        TakeDamage(clickDamage);
    }
}
