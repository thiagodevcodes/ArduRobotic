using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinParts: MonoBehaviour
{
    public string typePart;
    private FixedJoint joint;

    [SerializeField] private bool isJoint = false;

    private readonly HashSet<string> tagsJoints = new()
    {
        "JuntaHead", "JuntaArmEsq", "JuntaArmDir", "JuntaLegEsq", "JuntaLegDir"
    };

    //Juntar a peça do robo 
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
            
            //Se possui o componente, é possível conectar e ainda não está junto segue.
            if (jointConnection != null && jointConnection.OnPossibleConnect(typePart) && isJoint == false)
            {
                //Pega o transform do objeto colidido e do objeto atual
                Transform parentRobot = collision.transform;
                Transform transformObject = transform;

                //Transforma o objeto o objeto colidido em pai do objeto atual
                transformObject.parent = parentRobot.parent;
                transformObject.position = collision.transform.position;

                //Inicia o processo de junção
                joint = transformObject.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRigidbody;
             
                isJoint = true;
            }
        }
    }

    //Verificar a colisão
    void OnCollisionEnter(Collision collision)
    {
        if (!isJoint && tagsJoints.Contains(collision.gameObject.tag))
        {
            JointPart(collision);
        }
    }
}
