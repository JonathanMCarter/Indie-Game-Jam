using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CarterGames.Assets.SaveManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private LevelExit exit;
        private SaveData _data;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            _data = SaveManager.LoadGame();
        }


        private void Update()
        {
            if (!exit && !SceneManager.GetActiveScene().name.Equals("Menu"))
            {
                exit = FindObjectOfType<LevelExit>();
            }
        }


        public void LevelFailed()
        {
            StartCoroutine(LevelReset());
        }


        private IEnumerator LevelReset()
        {
            exit.FadeOutLevel();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        public void LevelWon()
        {
            StartCoroutine(NextLevel());
        }


        private IEnumerator NextLevel()
        {
            exit.FadeOutLevel();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        public void LoadLastLevel()
        {
            _data = SaveManager.LoadGame();
            SceneManager.LoadScene(_data.lastLevel);
        }


        public void ExitGame()
        {
            Application.Quit();
        }
    }
}