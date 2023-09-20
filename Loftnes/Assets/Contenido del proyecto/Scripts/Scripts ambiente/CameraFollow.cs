using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target_player; // Referencia al objeto que la cámara seguirá

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

        // Establecer la posición inicial de la cámara
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

                // Ajustar la posición horizontal de la cámara si el objetivo está dentro de los límites
                if (target_posX > derechaMax && target_posX < izquierdaMAx)
                {
                    posX = target_posX;
                }

                // Ajustar la posición vertical de la cámara si el objetivo está dentro de los límites
                if (target_posY < alturaMax && target_posY > alturaMin)
                {
                    posY = target_posY;
                }
            }

            // Mover suavemente la cámara hacia la nueva posición
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed * Time.deltaTime);
        }
    }
    void FollowPlayer()
    {
        if (target_player != null)
        {
            // Obtén la posición actual de la cámara.
            Vector3 posicionActual = transform.position;

            // Calcula la posición deseada de la cámara.
            Vector3 posicionDeseada = target_player.transform.position;

            // Mantén la misma z que la cámara para mantenerla en la misma altura en una vista top-down.
            posicionDeseada.z = posicionActual.z;

            // Interpola suavemente entre la posición actual de la cámara y la posición deseada.
            // Esto crea un efecto suave de seguimiento.
            transform.position = Vector3.Lerp(posicionActual, posicionDeseada, speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        //Move_CamLimit(); // Llamar a la función de seguimiento de la cámara en cada fotograma
        FollowPlayer();
    }
}
