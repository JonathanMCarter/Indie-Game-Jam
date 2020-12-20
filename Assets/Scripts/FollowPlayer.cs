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
        [SerializeField] private Vector3 startPos;


        private void Update()
        {
            if (!transform.position.Equals(toFollow.position + startPos))
            {
                transform.position = toFollow.position + startPos;
            }
        }
    }
}