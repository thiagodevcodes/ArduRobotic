using UnityEngine;

public class ControlJoints: MonoBehaviour
{
    public string typeJoint; // Por exemplo, "JuntaHead", "JuntaArmEsq", etc.
    public bool isJoint = false;
    public bool isDestroy = true;
    
    //Verifica se o tipo de pe�a � igual ao tipo de junta para fazer a conex�o
    public bool OnPossibleConnect(string typePart)
    {
        return typePart == typeJoint && isJoint == false;
    }
}