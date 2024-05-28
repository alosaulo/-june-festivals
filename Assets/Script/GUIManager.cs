using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    
    public GameObject pnlWin;
    public GameObject pnlGameOver;

    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtFished;
    public TextMeshProUGUI txtTrys;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(gameManager.time);
        txtTime.text = time.ToString("hh':'mm':'ss");
        txtFished.text ="Pescados: "+ gameManager.fished.ToString()+"/ 10";
        txtTrys.text = "Tentativas: "+gameManager.trys.ToString();

        if (gameManager.gameover) 
        {
            ActiveGameOverPnl();
        }

        if (gameManager.winrar) 
        {
            ActiveWinPnl();
        }
    }

    public void ActiveWinPnl() 
    { 
        pnlWin.SetActive(true);
    }

    public void ActiveGameOverPnl() 
    {
        pnlGameOver.SetActive(true);  
    }

}
