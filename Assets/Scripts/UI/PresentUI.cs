using UnityEngine;
using TMPro;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class PresentUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;


        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }


        /// <summary>
        /// Sets the UI to the value.
        /// </summary>
        /// <param name="value"></param>
        public void SetPresentUIValue(int value)
        {
            text.text = string.Format("x {0}", value);
        }


        public void SetPresentUIValue(int value, string customString)
        {
            text.text = string.Format(customString, value);
        }
    }
}