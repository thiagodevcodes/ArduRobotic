using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinObjects : MonoBehaviour
{
    public GameObject objectToJoin; // O objeto que será unido a este objeto
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "BracoEsq")
        {
            // Desabilita a física do objeto a ser unido
            collision.rigidbody.isKinematic = true;

            // Define o objeto a ser unido como pai deste objeto
            collision.transform.SetParent(transform);
        }
    }
}


