using UnityEngine;

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

        private bool hasLevelEnded;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                hasLevelEnded = true;
                tc.isRunning = false;
            }
        }
    }
}