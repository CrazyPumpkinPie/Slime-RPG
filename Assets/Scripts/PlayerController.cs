using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float hp = 100,hpReg = 3, attack = 50,attackSPD = 0.6f;
    private float attackDistance = 15;

    [SerializeField] private GameObject projectile;
    private GameObject[] projectilePool;
    private int currentProjectile = 0, projectilePoolSize = 15;

    private Rigidbody projectileRb;
    private bool isProjectileFlying,isGettingDamage,isHealing;

    private static PlayerController player;
    public static PlayerController Player { get { return player; }}
    private void Awake()
    {
        player = this;
    }
    void Start()
    {
        isProjectileFlying = false;
        isGettingDamage = false;
        isHealing = false;

        projectileRb = projectile.GetComponent<Rigidbody>();
        projectilePool = new GameObject[projectilePoolSize];

        for (int i = 0; i < projectilePoolSize; i++)
        {
            projectilePool[i] = Instantiate(projectile);
            projectilePool[i].gameObject.SetActive(false);
        }

        Healthbar.SetNewHP(gameObject);
    }

    void Update()
    {
        if (!isProjectileFlying && !GameManager.Instance.isGameOver && GameObject.FindGameObjectWithTag("Enemy").transform.position.x - projectileRb.position.x <= attackDistance)
            StartCoroutine(Shoot());
        else StopCoroutine(Shoot());

        if (GameManager.Instance.isFighting && !isGettingDamage && !GameManager.Instance.isGameOver)
            StartCoroutine(Fight());
        else StopCoroutine(Fight());

        if (gameObject.GetComponent<Healthbar>().healthbar.value < gameObject.GetComponent<Healthbar>().healthbar.maxValue && !isHealing && !GameManager.Instance.isGameOver)
            StartCoroutine(HPRegen());
        else StopCoroutine(HPRegen());
    }
    IEnumerator Shoot()
    {
        if (currentProjectile >= projectilePoolSize)
            currentProjectile = 0;

        isProjectileFlying = true;
        
        projectilePool[currentProjectile].SetActive(true);
        projectilePool[currentProjectile].transform.position = transform.position + new Vector3(0,1,0);
        currentProjectile++;
        yield return new WaitForSeconds(1/attackSPD);

        isProjectileFlying = false;
    }
    IEnumerator Fight()
    {
        isGettingDamage = true;

        Healthbar.UpdateHealth(gameObject, EnemyController.attack * spawnManager.enemyCount);
        yield return new WaitForSeconds(1f);

        isGettingDamage = false;
    }
    IEnumerator HPRegen()
    {
        isHealing = true;

        gameObject.GetComponent<Healthbar>().healthbar.value += hpReg;
        yield return new WaitForSeconds(1);

        isHealing = false;
    }
}
