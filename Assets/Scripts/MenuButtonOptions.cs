using UnityEngine;
using System.IO;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class MenuButtonOptions : MonoBehaviour
    {
        [SerializeField] private GameObject[] buttonOptions;


        private void Start()
        {
#if UNITY_WEBGL
            buttonOptions[1].SetActive(true);
#else
            string SavePath = Application.persistentDataPath + "/savefile.sf";

            if (File.Exists(SavePath))
            {
                buttonOptions[0].SetActive(true);
            }
            else
            {
                buttonOptions[1].SetActive(true);
            }
#endif
        }
    }
}