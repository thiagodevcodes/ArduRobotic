using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRobot : MonoBehaviour
{
    public string tipoDaJunta; // Por exemplo, "JuntaHead", "JuntaArmEsq", etc.
    public bool isJunto = false;

    public bool PodeConectar(string tipoDaPeca)
    {
        return tipoDaPeca == tipoDaJunta;
    }
}