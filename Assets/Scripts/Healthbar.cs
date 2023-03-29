using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Slider healthbar;
    void Start()
    {

    }
    public static void UpdateHealth(GameObject gameObject,float damage)
    {
        if (damage > gameObject.GetComponent<Healthbar>().healthbar.value)
        {
            gameObject.GetComponent<Healthbar>().healthbar.value = 0;
            if (gameObject.GetComponent<EnemyController>())
            {
                gameObject.SetActive(false);
            }
            else GameManager.Instance.GameOver();

            gameObject.GetComponent<Healthbar>().healthbar.value = gameObject.GetComponent<Healthbar>().healthbar.maxValue;

            int coinPerWave = 10 + spawnManager.waveNumber;
            GameManager.Instance.IncreaseCoinAmount(coinPerWave);
        }
        else gameObject.GetComponent<Healthbar>().healthbar.value -= damage;
    }

    public static void SetNewHP(GameObject gameObject)
    {
        if (gameObject.GetComponent<EnemyController>())
            gameObject.GetComponent<Healthbar>().healthbar.maxValue = EnemyController.hp;
        else gameObject.GetComponent<Healthbar>().healthbar.maxValue = PlayerController.hp;

        gameObject.GetComponent<Healthbar>().healthbar.value = gameObject.GetComponent<Healthbar>().healthbar.maxValue;
        //Debug.Log(gameObject.name + " has " + gameObject.GetComponent<Healthbar>().healthbar.maxValue + " hp" + "\n" + "and " + EnemyController.attack + " attack");
    }




    /*
if(gameObject.GetComponent<EnemyController>())
    healthbar.maxValue = EnemyController.hp;
else healthbar.maxValue = PlayerController.hp;
    currentHealth = healthbar.maxValue;

healthbar.value = currentHealth;
*/
    /*
    public void UpdateHealth(float damage)
    {
        if (damage > currentHealth)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
            currentHealth = healthbar.maxValue;
        }
        else currentHealth -= damage;

        healthbar.value = currentHealth;
    }
     */
    /*
         private void OnTriggerEnter(Collider other)
    {
        if(gameObject.GetComponent<EnemyController>())
            UpdateHealth(PlayerController.attack);
        else UpdateHealth(EnemyController.attack);

        if (!other.CompareTag("Ground"))
            other.gameObject.SetActive(false);
    }
     */
}
