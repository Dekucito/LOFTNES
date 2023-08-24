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

        // Establecer la posición inicial de la cámara
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

    private void Update()
    {
        Move_Cam(); // Llamar a la función de seguimiento de la cámara en cada fotograma
    }
}
