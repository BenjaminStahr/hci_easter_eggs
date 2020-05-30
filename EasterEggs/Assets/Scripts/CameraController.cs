using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    GameObject SearchKnob;
    public int EggCounter = 0;
    public bool changePlatform = false;
    public Vector3 OneFloor = new Vector3(0, 0, 0);
    //public Material KnobMaterial;
    // Start is called before the first frame update
    void Start()
    {
        SearchKnob = GameObject.FindGameObjectWithTag("SearchKnob");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] EasterEggs = GameObject.FindGameObjectsWithTag("EasterEgg");
        Vector3 EasterEggVector = EasterEggs[0].transform.position - this.transform.position;
        GameObject EasterEgg = EasterEggs[0];
        for (int i = 0; i < EasterEggs.Length; i++)
        {
            Vector3 EasterEggVectorTemp = EasterEggs[i].transform.position - this.transform.position;
            float AngleEasterEggTemp = Vector3.Angle(EasterEggVectorTemp, transform.TransformDirection(Vector3.forward));
            float AngleEasterEggBest = Vector3.Angle(EasterEggVector, transform.TransformDirection(Vector3.forward));
            if (AngleEasterEggTemp < AngleEasterEggBest)
            {
                EasterEgg = EasterEggs[i];
            }

        }
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
                if(EggCounter % 3 == 0 && EggCounter != 9)
                {
                    changePlatform = true;
                }
            }
        }
        if (changePlatform)
        {
            this.gameObject.transform.parent.transform.Translate(Vector3.up * 10 * Time.deltaTime);
            OneFloor += Vector3.up * 10 * Time.deltaTime;
            if (OneFloor.magnitude > 8)
            {
                OneFloor = new Vector3(0, 0, 0);
                changePlatform = false;
            }
        }
    }
}
