using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class TpPortal : MonoBehaviour
    {
        [Header("VariablesCambioDeColor")]
        public SpriteRenderer[] sprites;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;
        public Color color;

        [Header("scripts")]
        public PlayerMovement player;

        [Header("bools")]
        public bool panelActive;
        public bool thisTpIsActive;
        public bool previousDisabled;

        [Header("Tp")]
        public GameObject panel;

        public AudioClip[] sonidosTP;
        public AudioSource source { get { return GetComponent<AudioSource>(); } }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            gameObject.AddComponent<AudioSource>();
        }
        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in sprites)
            {
                r.color = curColor;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            Teleport();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
        }

        private void Teleport()
        {
            if (thisTpIsActive)
            {
                if (!previousDisabled)
                {
                    StartCoroutine(RutineSpritesActive());
                }
                if (panelActive)
                {
                    if (Input.GetButton("Interact"))
                    {
                        //panel.SetActive(true);
                        player.playerIsWalking = false;
                        player.CanMove = false;
                    }
                }
            }
            else if (!thisTpIsActive)
            {
                StartCoroutine(RutineSpritesDesactive());
            }
           
        }
        private void SpritesAnimationEnter()
        {
            if (thisTpIsActive)
            {
                targetColor = color;
            }
        }
        IEnumerator RutineSpritesActive()
        {
   
            source.PlayOneShot(sonidosTP[0]); // el [0] es de entrada a el portal

            yield return new WaitForSeconds(1);

            SpritesAnimationEnter();

            yield return new WaitForSeconds(0.1f);

            panelActive = true;
        }
        IEnumerator RutineSpritesDesactive()
        {
            previousDisabled = true;
            source.PlayOneShot(sonidosTP[1]); // el [1] es de activacion del portal
            yield return new WaitForSeconds(1);

            panelActive = true;
            thisTpIsActive = true;

            yield return new WaitForSeconds(0.2f);
           
            SpritesAnimationEnter();
            Teleport();
            previousDisabled = false;
        }
    }
}
