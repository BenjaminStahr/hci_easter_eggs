using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    GameObject SearchKnob;
    //public Material KnobMaterial;
    // Start is called before the first frame update
    void Start()
    {
        SearchKnob = GameObject.FindGameObjectWithTag("SearchKnob");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject EasterEgg = GameObject.FindGameObjectWithTag("EasterEgg");
        Vector3 EasterEggVector = EasterEgg.transform.position - this.transform.position;
        float AngleEasterEgg = Vector3.Angle(EasterEggVector, transform.TransformDirection(Vector3.forward));
        if (AngleEasterEgg < 60)
        {
            Debug.Log("Value : "+(255 - (60 - (AngleEasterEgg / 3)) * 4.25));
            Debug.Log("Angle : "+AngleEasterEgg);
            SearchKnob.GetComponent<GvrReticlePointer>().MaterialComp.color = new Color32(255, (byte)(255 - (60 - AngleEasterEgg) * 4.25), (byte)(255 - (60 - AngleEasterEgg) * 4.25), 255);
        }
        else
        {
            SearchKnob.GetComponent<GvrReticlePointer>().MaterialComp.color = new Color32(255, 255, 255, 255);
        }
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, ~0))
        {
            if (hit.transform.gameObject.tag == "EasterEgg")
            {
                hit.transform.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
