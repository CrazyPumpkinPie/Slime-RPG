using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public bool isGameOver,isFighting;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private  TextMeshProUGUI coinAmountText;
    private int coinAmount = 0;

    [SerializeField] private Button enhanceATKButton;
    [SerializeField] private TextMeshProUGUI enhanceATKText, ATKAmountText, ATKLVLText;
    private int ATKPrice = 5,ATKLVL = 1;

    [SerializeField] private Button enhanceHPButton;
    [SerializeField] private TextMeshProUGUI enhanceHPText,HPAmountText, HPLVLText;
    private int HPPrice = 5,HPLVL = 1;

    [SerializeField] private Button enhanceHPRegButton;
    [SerializeField] private TextMeshProUGUI enhanceHPRegText,HPRegAmountText, HPRegLVLText;
    private int HPRegPrice = 5,HPRegLVL = 1;

    [SerializeField] private Button enhanceATKSPDButton;
    [SerializeField] private TextMeshProUGUI enhanceATKSPDText, ATKSPDAmountText, ATKSPDLVLText;
    private int ATKSPDPrice = 15, ATKSPDLVL = 1;

    [SerializeField] private GameObject gameOverPanel;
    private void Awake()
    {
        instance = this;
    }
    void Start()    
    {
        ATKAmountText.text = "ATK" + "\n" + PlayerController.attack.ToString();
        ATKLVLText.text = "LVL " + ATKLVL.ToString();
        enhanceATKText.text = "Enhance" + "\n" + ATKPrice;

        HPAmountText.text = "HP" + "\n" + PlayerController.hp.ToString();
        HPLVLText.text = "LVL " + HPLVL.ToString();
        enhanceHPText.text = "Enhance" + "\n" + HPPrice;

        HPRegAmountText.text = "HPReg" + "\n" + PlayerController.hpReg.ToString();
        HPRegLVLText.text = "LVL " + HPRegLVL.ToString();
        enhanceHPRegText.text = "Enhance" + "\n" + HPRegPrice;

        ATKSPDAmountText.text = "ATKSPD" + "\n" + PlayerController.attackSPD.ToString();
        ATKSPDLVLText.text = "LVL " + ATKSPDLVL.ToString();
        enhanceATKSPDText.text = "Enhance" + "\n" + ATKSPDPrice;

        coinAmountText.text = coinAmount.ToString();
    }
    void Update()
    {
        if (ATKPrice > coinAmount)
            enhanceATKButton.interactable = false;
        else enhanceATKButton.interactable = true;

        if (HPPrice > coinAmount)
            enhanceHPButton.interactable = false;
        else enhanceHPButton.interactable = true;

        if (HPRegPrice > coinAmount)
            enhanceHPRegButton.interactable = false;
        else enhanceHPRegButton.interactable = true;

        if (ATKSPDPrice > coinAmount)
            enhanceATKSPDButton.interactable = false;
        else enhanceATKSPDButton.interactable = true;
    }

    public void Upgrade(string paramName)
    {
        switch(paramName)
        {
            case "ATK":
                IncreaseCoinAmount(-ATKPrice);

                PlayerController.attack += 10 + ATKLVL;
                ATKAmountText.text = "ATK" + "\n" + PlayerController.attack.ToString();

                ATKLVL++;
                ATKLVLText.text = "LVL " + ATKLVL.ToString();

                ATKPrice += 5 * ATKLVL;
                enhanceATKText.text = "Enhance" + "\n" + ATKPrice;
                break;

            case "HP":
                IncreaseCoinAmount(-HPPrice);

                PlayerController.hp += 10*HPLVL;
                //Healthbar.SetNewHP(PlayerController.Player.gameObject);
                HPAmountText.text = "HP" + "\n" + PlayerController.hp.ToString();

                HPLVL++;
                HPLVLText.text = "LVL " + HPLVL.ToString();

                HPPrice += 5 * HPLVL;
                enhanceHPText.text = "Enhance" + "\n" + HPPrice;
                break;

            case "HPReg":
                IncreaseCoinAmount(-HPRegPrice);

                PlayerController.hpReg += 0.1f*HPRegLVL;
                HPRegAmountText.text = "HPReg" + "\n" + PlayerController.hpReg.ToString();

                HPRegLVL++;
                HPRegLVLText.text = "LVL " + HPRegLVL.ToString();

                HPRegPrice += 5 * HPRegLVL;
                enhanceHPRegText.text = "Enhance" + "\n" + HPRegPrice;
                break;

            case "ATKSPD":
                IncreaseCoinAmount(-ATKSPDPrice);

                PlayerController.attackSPD += PlayerController.attackSPD*0.1f;
                ATKSPDAmountText.text = "ATKSPD" + "\n" + PlayerController.attackSPD.ToString();

                ATKSPDLVL++;
                ATKSPDLVLText.text = "LVL " + ATKSPDLVL.ToString();

                ATKSPDPrice += 15 * ATKSPDLVL;
                enhanceATKSPDText.text = "Enhance" + "\n" + ATKSPDPrice;
                break;
        }
    }

    public void IncreaseCoinAmount(int gain)
    {
        coinAmount += gain;
        coinAmountText.text = coinAmount.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }
    public void Continue()
    {
        Debug.Log(spawnManager.waveNumber);
        for (int i = 0; i < 4; i++)
        {
            EnemyController.attack -= 0.1f * (spawnManager.waveNumber - i);
            EnemyController.hp -= 2 * (spawnManager.waveNumber - i);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            enemy.SetActive(false);

        spawnManager.waveNumber -= 4;
        Healthbar.SetNewHP(PlayerController.Player.gameObject);
        gameOverPanel.SetActive(false);
        isGameOver = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
