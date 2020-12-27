using UnityEngine;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class Present : MonoBehaviour
    {
        private GameManager gm;
        private AudioManager am;


        private void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            am = FindObjectOfType<AudioManager>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                am.Play("ding", .75f);
                gm.AddPresents();
                gameObject.SetActive(false);
            }
        }
    }
}