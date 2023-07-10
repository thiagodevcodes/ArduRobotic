using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joints : MonoBehaviour
{
    FixedJoint joint;

    void OnCollisionEnter(Collision collision)
    {
        // Verifique se a colisão ocorreu com o objeto desejado
        if (collision.gameObject.CompareTag("Junta"))
        {
            Debug.Log("Objeto que colide: " + gameObject.name);
            Debug.Log("Objeto colidido: " + collision.gameObject.name);
            gameObject.transform.position = collision.gameObject.transform.position;

            // Obtenha uma referência ao RigidBody do objeto atual
            Rigidbody rb = GetComponent<Rigidbody>();

            // Obtenha uma referência ao RigidBody do objeto colidido
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();


            if(joint == null)
            {
                // Crie uma junta (joint) para conectar os dois objetos
                joint = gameObject.AddComponent<FixedJoint>();
                // Conecte os dois objetos com a junta (joint)
                joint.connectedBody = otherRb;
            }                        
        }
    }
}
