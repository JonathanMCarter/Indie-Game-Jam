using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class Present : MonoBehaviour
    {
        private GameManager gm;


        private void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gm.AddPresents();
                gameObject.SetActive(false);
            }
        }
    }
}