using UnityEngine;
using TMPro;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class PresentUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }


        /// <summary>
        /// Sets the UI to the 
        /// </summary>
        /// <param name="value"></param>
        public void SetPresentUIValue(int value)
        {
            text.text = string.Format("x {0}", value);
        }
    }
}