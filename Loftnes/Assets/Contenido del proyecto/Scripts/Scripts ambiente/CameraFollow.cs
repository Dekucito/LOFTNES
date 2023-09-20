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

    private float derechaMax;
    private float izquierdaMAx;

    private float alturaMax;
    private float alturaMin;

    public float speed;
    public bool encendida = true;

    private void Awake()
    {
        posX = target_posX + derechaMax;
        posY = target_posY + alturaMin;

        // Establecer la posici�n inicial de la c�mara
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }

    void Move_CamLimit()
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
    void FollowPlayer()
    {
        if (target_player != null)
        {
            // Obt�n la posici�n actual de la c�mara.
            Vector3 posicionActual = transform.position;

            // Calcula la posici�n deseada de la c�mara.
            Vector3 posicionDeseada = target_player.transform.position;

            // Mant�n la misma z que la c�mara para mantenerla en la misma altura en una vista top-down.
            posicionDeseada.z = posicionActual.z;

            // Interpola suavemente entre la posici�n actual de la c�mara y la posici�n deseada.
            // Esto crea un efecto suave de seguimiento.
            transform.position = Vector3.Lerp(posicionActual, posicionDeseada, speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        //Move_CamLimit(); // Llamar a la funci�n de seguimiento de la c�mara en cada fotograma
        FollowPlayer();
    }
}
