using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinParts: MonoBehaviour
{
    public string typePart;
    private FixedJoint joint;

    private GameObject objetoDeColisao;

    private readonly HashSet<string> tagsJoints = new()
    {
        "JuntaHead", "JuntaArmEsq", "JuntaArmDir", "JuntaLegEsq", "JuntaLegDir"
    };

    //Juntar a pe�a do robo 
    private void JointPart(Collision collision)
    {
        //Capturar o rigidbody do objeto colidido
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        
        //Se possuir rigidbody entra
        if (otherRigidbody != null)
        {
            //Capturar o componente controlJoints do objeto colidido
            ControlJoints jointConnection = collision.gameObject.GetComponent<ControlJoints>();
            
            Debug.Log(jointConnection);
            Debug.Log(jointConnection.OnPossibleConnect(typePart));
            
            //Se possui o componente, � poss�vel conectar e ainda n�o est� junto segue.
            if (jointConnection != null && jointConnection.OnPossibleConnect(typePart))
            {
                Debug.Log("Entrou aqui");
                //Pega o transform do objeto colidido e do objeto atual
                Transform parentRobot = collision.transform;
                Transform transformObject = transform;

                //Transforma o objeto o objeto colidido em pai do objeto atual
                transformObject.parent = parentRobot.parent;
                transformObject.position = collision.transform.position;

                //Inicia o processo de jun��o
                joint = transformObject.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRigidbody;

                collision.gameObject.GetComponent<ControlJoints>().isJoint = true;
                
            }
        }
    }

    void OnDestroy()
    { 
        Debug.Log("Objeto destruido " + objetoDeColisao.name);

        if (objetoDeColisao != null && objetoDeColisao.name == typePart)
        {
            ControlJoints controlJoints = objetoDeColisao.GetComponent<ControlJoints>();

            if (controlJoints != null)
            {
                controlJoints.isJoint = false; // Modifique conforme necess�rio
            }
            else
            {
                Debug.LogWarning("O componente ControlJoints n�o foi encontrado no objeto de colis�o.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum objeto de colis�o armazenado.");
        }
    }

    //Verificar a colis�o
    void OnCollisionEnter(Collision collision)
    {
        if (tagsJoints.Contains(collision.gameObject.tag) && collision.gameObject.GetComponent<ControlJoints>().isJoint == false)
        {
            if(collision.gameObject.name == typePart)
            {
                objetoDeColisao = collision.gameObject;
            }
            
            JointPart(collision);
        }
    }
}
