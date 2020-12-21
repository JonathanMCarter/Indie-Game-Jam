using UnityEngine;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class LevelExit : MonoBehaviour
    {
        [SerializeField] private TurnController tc;
        [SerializeField] private FadeOutOnEnd[] fadeOut;

        private bool hasLevelEnded;


        private void Start()
        {
            fadeOut = FindObjectsOfType<FadeOutOnEnd>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FadeOutLevel();
                hasLevelEnded = true;
                tc.isRunning = false;
            }
        }


        private void FadeOutLevel()
        {
            for (int i = 0; i < fadeOut.Length; i++)
            {
                fadeOut[i].PerformEndEffect();
            }
        }
    }
}