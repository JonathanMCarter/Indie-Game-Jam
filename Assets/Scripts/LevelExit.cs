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
        [SerializeField] private GameObject[] toDisable;


        private void Start()
        {
            fadeOut = FindObjectsOfType<FadeOutOnEnd>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FadeOutLevel();
                DisableObjects();
                tc.isRunning = false;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneChanger>().LevelWon();
            }
        }


        public void FadeOutLevel()
        {
            for (int i = 0; i < fadeOut.Length; i++)
            {
                fadeOut[i].PerformEndEffect();
            }
        }


        private void DisableObjects()
        {
            for (int i = 0; i < toDisable.Length; i++)
            {
                toDisable[i].SetActive(false);
            }
        }
    }
}