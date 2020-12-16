using System.Collections;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class Despawner : MonoBehaviour
    {
        [Header("Despawn Delay")][Tooltip("Set this to define how long the object will wait before despawning. Default Value = 1")]
        [SerializeField] private float despawnTime = 1f;
        [Header("Despawn Choices")][Tooltip("Tick this if the object should be disabled when the timer runs out.")]
        [SerializeField] private bool disable = false;
        [Tooltip("Tick this if the object should be destroyed when the timer runs out.")]
        [SerializeField] private bool destroy = false;


        /// <summary>
        /// When the object is enabled, start the corutine that will despawn the object.
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(DespawnCo());
        }

        /// <summary>
        /// When the object is disabled, stop all corutines so it doesn't run more than it needs to.
        /// </summary>
        private void OnDisable()
        {
            StopAllCoroutines();
        }


        /// <summary>
        /// Despawns the object this is attached to as and when it is enabled.
        /// </summary>
        private IEnumerator DespawnCo()
        {
            yield return new WaitForSeconds(despawnTime);

            if (disable)
            {
                gameObject.SetActive(false);
            }

            if (destroy)
            {
                Destroy(gameObject);
            }
        }
    }
}