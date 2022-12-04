using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Movement.FootstepController
{
    public class FootstepController : MonoBehaviour
    {
        public AudioClip footStepSound;
        public float footStepDelay;

        private float nextFootstep = 0;

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                nextFootstep -= Time.deltaTime;
                if ( nextFootstep <= 0)
                {
                    GetComponent<AudioSource>().PlayOneShot( footStepSound, 0.7f);
                    nextFootstep += footStepDelay;
                }
            }
        }
    }
}


