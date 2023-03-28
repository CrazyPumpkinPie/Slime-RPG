using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private float speed = 18;
    private float leftBound = -1;

    public static float hp = 100, attack = 20;
    void Start()
    {
        
    }

    void Update()
    {
        MoveForward();

        if (transform.position.x <= leftBound)
            GameManager.Instance.isFighting = true;
    }

    void MoveForward()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isFighting && transform.position.x >= leftBound)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {

        Destroy(other.gameObject);
    }
}
