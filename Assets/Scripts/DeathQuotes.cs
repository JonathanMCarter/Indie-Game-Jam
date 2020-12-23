using UnityEngine;
using TMPro;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class DeathQuotes : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string[] quotes;


        private void Start()
        {
            if (TryGetComponent(out TMP_Text text))
            {
                _text = GetComponent<TMP_Text>();
            }
        }

        public void PlayerDied()
        {
            _text.text = quotes[GetRandom.Int(0, quotes.Length - 1)];
        }
    }
}