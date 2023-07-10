using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuntarPecas : MonoBehaviour
{
    public Transform jointPoint; // Ponto de junção dos objetos

    private bool isColliding = false; // Verifica se está ocorrendo colisão
    private bool isJoined = false; // Verifica se os objetos estão juntos

    private void Update()
    {
        if (isColliding && !isJoined)
        {
            // Verifica se os objetos estão colidindo e ainda não estão juntos

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Verifica se um toque foi detectado

                // Move o objeto atual para o ponto de junção
                transform.position = jointPoint.position;
                transform.rotation = jointPoint.rotation;

                // Define o objeto atual como filho do ponto de junção
                transform.SetParent(jointPoint);

                isJoined = true; // Define a flag como true para indicar que os objetos estão juntos
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        // Verifica se ocorreu colisão com o objeto desejado
        if (collision.gameObject.CompareTag("ObjetoDesejado"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se a colisão com o objeto desejado cessou
        if (collision.gameObject.CompareTag("ObjetoDesejado"))
        {
            isColliding = false;
        }
    }
}
