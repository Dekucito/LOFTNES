using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target_player; // Referencia al objeto que la c�mara seguir�

    private float target_posX;
    private float target_posY;

    private float posX;
    private float posY;

    public float derechaMax;
    public float izquierdaMAx;

    public float alturaMax;
    public float alturaMin;

    public float speed;
    public bool encendida = true;

    private void Awake()
    {
        posX = target_posX + derechaMax;
        posY = target_posY + alturaMin;

        // Establecer la posici�n inicial de la c�mara
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }

    void Move_Cam()
    {
        if (encendida)
        {
            if (target_player)
            {
                target_posX = target_player.transform.position.x;
                target_posY = target_player.transform.position.y;

                // Ajustar la posici�n horizontal de la c�mara si el objetivo est� dentro de los l�mites
                if (target_posX > derechaMax && target_posX < izquierdaMAx)
                {
                    posX = target_posX;
                }

                // Ajustar la posici�n vertical de la c�mara si el objetivo est� dentro de los l�mites
                if (target_posY < alturaMax && target_posY > alturaMin)
                {
                    posY = target_posY;
                }
            }

            // Mover suavemente la c�mara hacia la nueva posici�n
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        Move_Cam(); // Llamar a la funci�n de seguimiento de la c�mara en cada fotograma
    }
}
