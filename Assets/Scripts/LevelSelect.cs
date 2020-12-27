using UnityEngine;
using CarterGames.Assets.SaveManager;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class LevelSelect : MonoBehaviour
    {
        [SerializeField] private GameObject[] padlocks;
        [SerializeField] private GameObject[] levelNumbers;
        private SaveData _data;

        public int levelsUnlocked;


        private void Start()
        {
            _data = SaveManager.LoadGame();

            if (!_data.presentsCollected.Equals(0))
                levelsUnlocked = int.Parse(_data.lastLevel.Substring(_data.lastLevel.Length - 1)) + 1;


            if (levelsUnlocked > 1)
            {
                for (int i = 0; i < padlocks.Length; i++)
                {
                    if (i + 1 <= levelsUnlocked)
                    {
                        levelNumbers[i].SetActive(true);
                        padlocks[i].SetActive(false);
                    }
                    else
                    {
                        levelNumbers[i].SetActive(false);
                        padlocks[i].SetActive(true);
                        padlocks[i].GetComponentInParent<UIBSButtonActions>().canPerformActions = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < padlocks.Length; i++)
                {
                    if (i.Equals(0))
                    {
                        levelNumbers[i].SetActive(true);
                        padlocks[i].SetActive(false);
                    }
                    else
                    {
                        levelNumbers[i].SetActive(false);
                        padlocks[i].SetActive(true);
                        padlocks[i].GetComponentInParent<UIBSButtonActions>().canPerformActions = false;
                    }
                }
            }
        }
    }
}