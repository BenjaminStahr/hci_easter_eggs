using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTimeScript : MonoBehaviour
{
    public float StartTime = 0;
    public bool WinGame = false;
    public bool InsideWinFirstTime = true;
    public float EndTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!WinGame)
        {
            TextMeshProUGUI textmeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
            textmeshProUGUI.SetText("{0}", (int)(Time.time - StartTime));
        }
        else
        {
            if (InsideWinFirstTime)
            {
                InsideWinFirstTime = false;
                EndTime = (int)(Time.time - StartTime);
            }
            TextMeshProUGUI textmeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
            textmeshProUGUI.SetText("Win! Time needed: "+ "{0}" + " Restart in 10 sec", EndTime);
        }
           
    }
}
