using UnityEngine;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public enum Moves { Up, Down, Left, Right, Attack, Defend, Steal, Leave }

    public class TurnController : MonoBehaviour
    {
        public List<GameObject> movesThisTurn;
        public bool isRunning;

        [SerializeField] private AudioSource source;
        [SerializeField] private float timeBetweenTurns;
        private float timer;


        private void Awake()
        {
            movesThisTurn = new List<GameObject>();
        }


        private void Update()
        {
            if (isRunning)
            {
                if (timer < timeBetweenTurns)
                {
                    timer += Time.deltaTime;
                }
                else if (timer > timeBetweenTurns)
                {
                    PerformMoves();
                    timer = 0;
                }
            }
        }


        /// <summary>
        /// Adds the player to the moves list so you make the move xD
        /// </summary>
        /// <param name="_object">this gameobject</param>
        public void AddMove(GameObject _object)
        {
            if (!movesThisTurn.Contains(_object))
                movesThisTurn.Add(_object);
        }


        /// <summary>
        /// Runs the moves for the enemies and player on time.
        /// </summary>
        private void PerformMoves()
        {
            for (int i = 0; i < movesThisTurn.Count; i++)
            {
                if (movesThisTurn[i].CompareTag("Player"))
                    movesThisTurn[i].GetComponent<PlayerController>().MakeMove();
                else
                    movesThisTurn[i].GetComponent<Enemy>().MakeMove();
            }

            movesThisTurn.Clear();
        }
    }
}