using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CarterGames.Assets.SaveManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private LevelExit exit;
        [SerializeField] private GameObject[] deathElements;
        [SerializeField] private DeathQuotes quotes;
        [SerializeField] private GameManager gm;
        [SerializeField] private SaveData _data;
        [SerializeField] private Animator trans;
        private TurnController tc;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            _data = SaveManager.LoadGame();
            tc = FindObjectOfType<TurnController>();

            if (GameObject.FindGameObjectWithTag("GameController"))
                gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

            deathElements = new GameObject[3];
            deathElements = GameObject.FindGameObjectsWithTag("DeathUI");

            for (int i = 0; i < deathElements.Length; i++)
            {
                deathElements[i].SetActive(false);
            }

            trans = GameObject.FindGameObjectWithTag("Trans").GetComponentInChildren<Animator>();
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
            tc.isRunning = false;
            trans.SetTrigger("Fade");

            for (int i = 0; i < deathElements.Length; i++)
            {
                deathElements[i].SetActive(true);
            }

            quotes.PlayerDied();
            gm.RemovePresent();

            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        public void LevelWon()
        {
            StartCoroutine(NextLevel());
        }


        private IEnumerator NextLevel()
        {
            gm.SavePresents();
            exit.FadeOutLevel();
            _data.lastLevel = SceneManager.GetActiveScene().name;
            SaveManager.SaveGame(_data);
            trans.SetTrigger("Fade");

            yield return new WaitForSeconds(1f);

            if (!SceneManager.GetActiveScene().name.Contains("5"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
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


        public void ResetPresentCounter()
        {
            SaveData _data = SaveManager.LoadGame();
            _data.presentsCollected = 0;
            SaveManager.SaveGame(_data);
        }


        public void GameOver()
        {
            StartCoroutine(IsGameOver());
        }


        private IEnumerator IsGameOver()
        {
            exit.FadeOutLevel();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("GameOver");
        }
    }
}