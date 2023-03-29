using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private float speed = 3;
    private float leftBound = -1;

    public static float hp = 80, attack = 3;

    [SerializeField] GameObject flyingText;

    private Animator enemyAnim;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();


    }

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        if (!GameManager.Instance.isGameOver)
        {
            if (transform.position.x >= leftBound)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                enemyAnim.SetFloat("Speed_f", 1.0f);
            }
            else
            {
                GameManager.Instance.isFighting = true;
                enemyAnim.SetFloat("Speed_f", 0f);
                enemyAnim.SetInteger("WeaponType_int", 10);
            }
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0f);
            enemyAnim.SetInteger("WeaponType_int", 0);
        }

     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Healthbar.UpdateHealth(gameObject, PlayerController.attack);
            other.gameObject.SetActive(false);

            GameObject damageNumber = Instantiate(flyingText, transform.position + new Vector3(0, 3, 0), flyingText.transform.rotation);
            damageNumber.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(PlayerController.attack.ToString());
            //FlyingNumbers.Instance.SendFlyingText(PlayerController.attack,transform.position + new Vector3(0,2,0));
        }
    }


}
