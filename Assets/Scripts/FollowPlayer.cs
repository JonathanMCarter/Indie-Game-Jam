using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform toFollow;

        private void Update()
        {
            if (!transform.position.Equals(toFollow.position))
            {
                transform.position = toFollow.position;
            }
        }
    }
}