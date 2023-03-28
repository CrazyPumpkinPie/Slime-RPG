using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public bool isGameOver,isFighting;

    private static GameManager instance;
    public static GameManager Instance {get{return instance;}}
    private void Awake()
    {
        instance = this;
    }
    void Start()    
    {
        
    }
    void Update()
    {

    }
}
