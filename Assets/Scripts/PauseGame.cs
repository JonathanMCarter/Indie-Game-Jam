using UnityEngine;
using UnityEngine.InputSystem;
using CarterGames.Utilities;
using System.Collections;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private Canvas pauseUI;
        private Actions actions;
        private bool isCoR;


        private void OnEnable()
        {
            actions = new Actions();
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }


        private void Update()
        {
            if (actions.Gameplay.Pause.phase.Equals(InputActionPhase.Performed) && !isCoR)
            {
                if (!pauseUI.enabled)
                {
                    pauseUI.enabled = true;
                    pauseUI.GetComponentInChildren<UIButtonSwitch>().enabled = true;
                    Time.timeScale = 0;
                    StartCoroutine(ButtonDelay());
                }
            }
        }


        private IEnumerator ButtonDelay()
        {
            isCoR = true;
            yield return new WaitForSecondsRealtime(.25f);
            isCoR = false;
        }


        public void ResumeGame()
        {
            pauseUI.enabled = false;
            pauseUI.GetComponentInChildren<UIButtonSwitch>().enabled = false;
            Time.timeScale = 1;
        }


        public void LoadMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}