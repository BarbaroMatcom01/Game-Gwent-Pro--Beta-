using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weathers : MonoBehaviour
{

   [SerializeField] GameObject weathers ;
    bool [] weatherIsActive= new bool[3];
    public bool Rain => weatherIsActive[0];
    public bool Storm =>weatherIsActive[1];
    public bool Snow => weatherIsActive[2];
}
