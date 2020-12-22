using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public enum Moves { Up, Down, Left, Right, Attack, Defend, Steal, Leave, None }

    public class TurnController : MonoBehaviour
    {
        public List<GameObject> movesThisTurn;
        public GameObject playerAction;
        public bool isRunning;

        [SerializeField] private AudioSource source;
        [SerializeField] private float timeBetweenTurns;
        private float timer;
        [SerializeField] private float actionTimer;
        [SerializeField] private float startDelay;


        private void Awake()
        {
            movesThisTurn = new List<GameObject>();
            StartCoroutine(StartDelay());
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
                    PerformEnemyActions();
                    PerformPlayerAction();
                    timer = 0;
                }

                if (actionTimer < timeBetweenTurns)
                {
                    actionTimer += Time.deltaTime;
                }
                else if (actionTimer > timeBetweenTurns)
                {
                    PerformPlayerAction();
                    actionTimer = 0;
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
        private void PerformEnemyActions()
        {
            for (int i = 0; i < movesThisTurn.Count; i++)
            {
                movesThisTurn[i].GetComponent<Enemy>().MakeMove();
            }

            movesThisTurn.Clear();
        }


        private void PerformPlayerAction()
        {
            if (playerAction)
            {
                playerAction.GetComponent<PlayerController>().MakeMove();
                playerAction = null;
            }
        }


        private IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(startDelay);
            isRunning = true;
        }
    }
}