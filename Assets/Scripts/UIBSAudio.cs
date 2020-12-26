using UnityEngine;
using CarterGames.Utilities;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class UIBSAudio : MonoBehaviour
    {
        [SerializeField] private string clipsNameToPlay;
        private AudioManager am;


        private void Awake()
        {
            am = FindObjectOfType<AudioManager>();
        }


        public void PlaySound()
        {
            am.Play(clipsNameToPlay);
        }
    }
}