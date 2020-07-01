using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    GameObject SearchKnob;
    public int EggCounter = 0;
    public bool changePlatform = false;
    public Vector3 OneFloor = new Vector3(0, 0, 0);
    public FloorNumber currentFloor = FloorNumber.first;

    // for loading new level
    public double StartLoadLevelTime = 0;
    public bool FirstTimeWin = true;

    void Start()
    {
        SearchKnob = GameObject.FindGameObjectWithTag("SearchKnob");
    }

    // Update is called once per frame
    void Update()
    {
        if (EggCounter == 9)
        {
            GameObject.FindGameObjectWithTag("Time").GetComponent<ShowTimeScript>().WinGame = true;
            GameObject.FindGameObjectWithTag("Rotation").GetComponent<ShowRotationScript>().WinGame = true;
            if (FirstTimeWin)
            {
                FirstTimeWin = false;
                StartLoadLevelTime = Time.time;
            }
            if (Time.time - StartLoadLevelTime > 10)
            {
                SceneManager.LoadScene(0);
            }
        }

        GameObject[] EasterEggs = GameObject.FindGameObjectsWithTag("EasterEgg");
        Vector3 EasterEggVector = EasterEggs[0].transform.position - this.transform.position;
        GameObject EasterEgg = null;
        for (int i = 0; i < EasterEggs.Length; i++)
        {
            if (EasterEggs[i].GetComponent<EggScript>().floor == currentFloor &&
                !EasterEggs[i].GetComponent<EggScript>().AlreadySeen)
            {
                EasterEgg = EasterEggs[i];
                break;
            }
        }
        for (int i = 0; i < EasterEggs.Length; i++)
        {
            Vector3 EasterEggVectorTemp = EasterEggs[i].transform.position - this.transform.position;
            float AngleEasterEggTemp = Vector3.Angle(EasterEggVectorTemp, transform.TransformDirection(Vector3.forward));
            float AngleEasterEggBest = Vector3.Angle(EasterEggVector, transform.TransformDirection(Vector3.forward));
            if (AngleEasterEggTemp < AngleEasterEggBest && EasterEggs[i].GetComponent<EggScript>().floor == currentFloor)
            {
                EasterEgg = EasterEggs[i];
            }

        }
        if (EasterEgg != null)
        {
            Vector3 EasterEggVectorFinal = EasterEgg.transform.position - this.transform.position;
            float AngleEasterEgg = Vector3.Angle(EasterEggVectorFinal, transform.TransformDirection(Vector3.forward));
            if (AngleEasterEgg < 60)
            {
                //Debug.Log("Value : "+(255 - (60 - (AngleEasterEgg / 3)) * 4.25));
                //Debug.Log("Angle : "+AngleEasterEgg);
                SearchKnob.GetComponent<GvrReticlePointer>().MaterialComp.color = new Color32(255, (byte)(255 - (60 - AngleEasterEgg) * 4.25), (byte)(255 - (60 - AngleEasterEgg) * 4.25), 255);
            }
            else
            {
                SearchKnob.GetComponent<GvrReticlePointer>().MaterialComp.color = new Color32(255, 255, 255, 255);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, ~0))
            {
                if (hit.transform.gameObject.tag == "EasterEgg"
                    && hit.transform.gameObject.GetComponent<EggScript>().AlreadySeen == false)
                {
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    hit.transform.GetComponent<EggScript>().AlreadySeen = true;
                    EggCounter++;
                    Destroy(hit.transform.gameObject, 5);
                    if (EggCounter % 3 == 0 && EggCounter != 9)
                    {
                        changePlatform = true;
                        if (currentFloor == FloorNumber.first)
                        {
                            currentFloor = FloorNumber.second;
                        }
                        else if (currentFloor == FloorNumber.second)
                        {
                            currentFloor = FloorNumber.third;
                        }
                    }

                }
            }
            if (changePlatform)
            {
                this.gameObject.transform.parent.transform.Translate(Vector3.up * 10 * Time.deltaTime);
                OneFloor += Vector3.up * 10 * Time.deltaTime;
                if (OneFloor.magnitude > 9)
                {
                    OneFloor = new Vector3(0, 0, 0);
                    changePlatform = false;
                }
            }
        }
    }
}
