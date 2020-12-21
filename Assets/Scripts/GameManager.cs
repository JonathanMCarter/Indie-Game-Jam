using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int presents;


        public void AddPresents()
        {
            presents++;
        }
    }
}