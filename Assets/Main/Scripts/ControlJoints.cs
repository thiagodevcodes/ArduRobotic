using UnityEngine;

public class ControlJoints: MonoBehaviour
{
    public string typeJoint; // Por exemplo, "JuntaHead", "JuntaArmEsq", etc.
    public bool isJoint = false;

    //Verifica se o tipo de peça é igual ao tipo de junta para fazer a conexão
    public bool OnPossibleConnect(string typePart)
    {
        //Se o tipo de junta for igual ao tipo de peça então retorna true
        return typePart == typeJoint;
    }
}