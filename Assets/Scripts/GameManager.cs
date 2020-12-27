using UnityEngine;
using UnityEngine.SceneManagement;
using CarterGames.Assets.SaveManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int presents;
        [SerializeField] private PresentUI[] presentUI;
        internal SaveData _data;


        private void Start()
        {
            _data = SaveManager.LoadGame();
            presents = _data.presentsCollected;

            if (presentUI[0])
            {
                presentUI[0].SetPresentUIValue(presents);
            }
        }


        private void Update()
        {
            if (presents < 0)
            {
                FindObjectOfType<SceneChanger>().GameOver();
            }
        }


        public void AddPresents()
        {
            presents++;
            presentUI[1].SetPresentUIValue(presents - _data.presentsCollected, "+ {0}");
        }


        public void SavePresents()
        {
            _data.presentsCollected = presents;
            SaveManager.SaveGame(_data);
        }


        public void RemovePresent()
        {
            presents--;
            presentUI[0].SetPresentUIValue(presents);
            _data.presentsCollected = presents;
            SaveManager.SaveGame(_data);
        }
    }
}