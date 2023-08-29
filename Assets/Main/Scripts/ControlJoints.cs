using UnityEngine;

public class ControlJoints: MonoBehaviour
{
    public string typeJoint; // Por exemplo, "JuntaHead", "JuntaArmEsq", etc.
    public bool isJoint = false;

    //Verifica se o tipo de pe�a � igual ao tipo de junta para fazer a conex�o
    public bool OnPossibleConnect(string typePart)
    {
        //Se o tipo de junta for igual ao tipo de pe�a ent�o retorna true
        return typePart == typeJoint;
    }
}