using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager Instance;

    public int fished;

    public int trys = 5;

    public float time = 120;

    public bool winrar;

    public bool gameover;

    public static GameManager GetInstance() { 
        return Instance;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0 && winrar == false) 
        {
            time -= Time.deltaTime;
        }

        if (fished >= 10) 
        {
            winrar = true;
        }

        if (trys <= 0 || time <= 0) 
        {
            gameover = true;
        }

    }

}
