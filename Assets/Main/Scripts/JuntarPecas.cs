using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                meuTransform.rotation = Quaternion.Euler(pai.rotation.x, pai.rotation.y, 0f);
                //meuTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

                joint = meuTransform.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = otherRigidbody;
             
                isJunto = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isJunto && tagsJuntas.Contains(collision.gameObject.tag))
        {
            JuntarPeca(collision);
        }
    }
}
