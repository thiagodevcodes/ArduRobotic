using UnityEngine;

public class ControlJoints: MonoBehaviour
{
    public string typeJoint; // Por exemplo, "JuntaHead", "JuntaArmEsq", etc.
    public bool isJoint = false;
    public bool isDestroy = true;
    
    //Verifica se o tipo de peça é igual ao tipo de junta para fazer a conexão
    public bool OnPossibleConnect(string typePart)
    {
        return typePart == typeJoint && isJoint == false;
    }
}