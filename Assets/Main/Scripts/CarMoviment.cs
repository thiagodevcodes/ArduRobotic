using UnityEngine;

public class CarMoviment : MonoBehaviour
{
    private Transform objectToMove;
    public float moveSpeed = 5f;
    public Joystick joystick;

    private void Update()
    {   
        if(!GameObject.FindGameObjectWithTag("Car")) return;

        objectToMove = GameObject.FindGameObjectWithTag("Car").transform;

        if (objectToMove)
        {
            // Obter os valores de entrada do joystick
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            // Calcular o vetor de movimento
            Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(horizontalInput, 0f, verticalInput);

            // Mover o objeto
            objectToMove.Translate(movement, Space.World);

            // Rotacionar o objeto na direção do movimento
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                objectToMove.rotation = Quaternion.Lerp(objectToMove.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

    }
}