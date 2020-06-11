using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGameScript : MonoBehaviour
{
    public bool Holding = false;
    public bool TimeSetted = false;
    public double time = 0;
    public int SceneNumber = 0;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Holding)
        {
            time = Time.time;
            Holding = false;
            TimeSetted = true;
        }
        if (Time.time - time > 2 && TimeSetted)
        {
            SceneManager.LoadScene(SceneNumber);
        }
    }
    public void SetHolding(int SceneNum)
    {
        Holding = true;
        SceneNumber = SceneNum;
    }
    public void UnsetHolding()
    {
        Holding = false;
        TimeSetted = false;
        time = 0;
    }
}
