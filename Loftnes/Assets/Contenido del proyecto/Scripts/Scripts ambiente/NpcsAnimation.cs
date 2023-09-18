using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcsAnimation : MonoBehaviour
{
    public Animator animator;
    public string[] animationNames;
    public float minWaitTime = 2.0f;
    public float maxWaitTime = 5.0f;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animationNames.Length == 0)
        {
            Debug.LogError("No se han configurado nombres de animaciones en el array.");
            enabled = false;
            return;
        }

        // Inicia la corrutina para reproducir animaciones aleatorias con tiempo de espera.
        StartCoroutine(PlayRandomAnimationWithDelay());
    }

    private IEnumerator PlayRandomAnimationWithDelay()
    {
        while (true)
        {
            // Selecciona aleatoriamente un nombre de animaci�n de la lista.
            string randomAnimation = animationNames[Random.Range(0, animationNames.Length)];

            // Reproduce la animaci�n seleccionada.
            animator.Play(randomAnimation);

            // Espera un tiempo aleatorio entre minWaitTime y maxWaitTime antes de la pr�xima animaci�n.
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
