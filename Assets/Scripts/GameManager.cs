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
        private string lastScene;
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
            if (!SceneManager.GetActiveScene().name.Equals(lastScene))
            {
                lastScene = SceneManager.GetActiveScene().name;
                presentUI = FindObjectOfType<PresentUI>();

                if (presentUI)
                {
                    presentUI.SetPresentUIValue(presents);
                }

                if (!lastScene.Equals("Menu"))
                {
                    _data.lastLevel = lastScene;
                    SaveManager.SaveGame(_data);
                }
            }
        }


        public void AddPresents()
        {
            presents++;
            presentUI.SetPresentUIValue(presents);
            _data.presentsCollected = presents;
        }
    }
}