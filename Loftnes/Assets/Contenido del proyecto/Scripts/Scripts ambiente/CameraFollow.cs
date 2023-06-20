using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player")]
    public GameObject target;

    private float target_poseX;
    private float target_poseY;

    [Header("camera")]
    public float posX;
    public float posY;

    public float speed;
    public bool encendida = true;

    [Header("limite")]
    public float derechaMax;
    public float izquierdaMax;

    public float alturaMax;
    public float alturaMin;

    private void Awake()
    {
        posX = target_poseX + derechaMax;
        posY = target_poseY + alturaMin;

        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }
    private void Update()
    {
        Move_Cam();
    }
    void Move_Cam()
    {
        if (encendida)
        {
            if (target)
            {
                target_poseX = target.transform.position.x;
                target_poseY = target.transform.position.y;

                if (target_poseX > derechaMax && target_poseX < izquierdaMax)
                {
                    posX = target_poseX;
                }
                if (target_poseY < alturaMax && target_poseY > alturaMin)
                {
                    posY = target_poseY;
                }
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed * Time.deltaTime);
        }
    }
}
