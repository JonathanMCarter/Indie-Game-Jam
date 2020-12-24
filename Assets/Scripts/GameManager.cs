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
        [SerializeField] private PresentUI presentUI;
        private SaveData _data;


        private void Start()
        {
            _data = SaveManager.LoadGame();
            presents = _data.presentsCollected;
            presentUI = FindObjectOfType<PresentUI>();
            presentUI.SetPresentUIValue(presents);
        }


        private void Update()
        {

            presentUI = FindObjectOfType<PresentUI>();

            if (presentUI)
            {
                presentUI.SetPresentUIValue(presents);
            }

            SaveManager.SaveGame(_data);


            if (presents < 0)
            {
                FindObjectOfType<SceneChanger>().GameOver();
            }
        }


        public void AddPresents()
        {
            presents++;
            presentUI.SetPresentUIValue(presents);
            _data.presentsCollected = presents;
        }


        public void RemovePresent()
        {
            presents--;
            presentUI.SetPresentUIValue(presents);
            _data.presentsCollected = presents;
            SaveManager.SaveGame(_data);
        }
    }
}