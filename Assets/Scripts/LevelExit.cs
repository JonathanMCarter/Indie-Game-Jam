using UnityEngine;
using CarterGames.Utilities;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class LevelExit : MonoBehaviour
    {
        [SerializeField] private TurnController tc;
        [SerializeField] private FadeOutOnEnd[] fadeOut;
        [SerializeField] private GameObject[] toDisable;
        private AudioManager am;


        private void Start()
        {
            fadeOut = FindObjectsOfType<FadeOutOnEnd>();
            tc = FindObjectOfType<TurnController>();
            am = FindObjectOfType<AudioManager>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                am.Play("door");
                FadeOutLevel();
                DisableObjects();
                tc.isRunning = false;
                GameObject.FindGameObjectWithTag("Changer").GetComponent<SceneChanger>().LevelWon();
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