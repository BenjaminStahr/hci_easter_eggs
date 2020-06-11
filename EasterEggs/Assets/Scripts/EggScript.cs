using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorNumber
{ 
    first,
    second,
    third
}

public class EggScript : MonoBehaviour
{
    public bool AlreadySeen = false;
    public FloorNumber floor = FloorNumber.first;
}
