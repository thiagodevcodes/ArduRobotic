using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuntarPecas : MonoBehaviour
{
    public string tipoDaPeca;
    private FixedJoint joint;

    [SerializeField] private bool isJunto = false;

    private readonly HashSet<string> tagsJuntas = new()
    {
        "JuntaHead", "JuntaArmEsq", "JuntaArmDir", "JuntaLegEsq", "JuntaLegDir"
    };

    //Juntar a peça do robo 
    private void JuntarPeca(Collision collision)
    {
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            ControlRobot conexaoJunta = collision.gameObject.GetComponent<ControlRobot>();
            Debug.Log(conexaoJunta);
            Debug.Log(conexaoJunta.PodeConectar(tipoDaPeca));
            
            if (conexaoJunta != null && conexaoJunta.PodeConectar(tipoDaPeca) && isJunto == false)
            {
                Transform pai = collision.transform;

                Transform meuTransform = transform;

                meuTransform.parent = pai.parent;
                meuTransform.position = collision.transform.position;

                joint = meuTransform.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRigidbody;
             
                isJunto = true;
            }
        }
    }

    //Verificar a colisão
    void OnCollisionEnter(Collision collision)
    {
        if (!isJunto && tagsJuntas.Contains(collision.gameObject.tag))
        {
            JuntarPeca(collision);
        }
    }
}
