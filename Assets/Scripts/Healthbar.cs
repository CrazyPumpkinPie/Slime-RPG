using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    private float currentHealth;

    void Start()
    {
        healthbar.maxValue = EnemyController.hp;
        currentHealth = healthbar.maxValue;
        healthbar.value = currentHealth;
    }
    public void UpdateHealth(float damage)
    {
        currentHealth -= damage;
        healthbar.value = currentHealth;
    }
}
