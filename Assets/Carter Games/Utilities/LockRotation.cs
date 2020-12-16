using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class LockRotation : MonoBehaviour
    {
        [Header("Local or World Space?")]
        [SerializeField] private bool useLocalRot;

        [Header("Late or standard Update Method?")]
        [SerializeField] private bool useLateUpdate;

        private Quaternion startRot;


        private void Start()
        {
            if (!useLocalRot)
                startRot = transform.rotation;
            else
                startRot = transform.localRotation;
        }


        private void Update()
        {
            if (!useLateUpdate)
            {
                if (!useLocalRot)
                    transform.rotation = startRot;
                else
                    transform.localRotation = startRot;
            }
        }


        private void LateUpdate()
        {
            if (useLateUpdate)
            {
                if (!useLocalRot)
                    transform.rotation = startRot;
                else
                    transform.localRotation = startRot;
            }
        }
    }
}