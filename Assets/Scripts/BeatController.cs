using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class BeatController : MonoBehaviour
    {
        public AudioClip SongToPlay;

        public float Song_BPM;
        public float SecPerBeat;
        public float Song_Pos;
        public float Song_Pos_Beats;
        public float DSPSongTime;
        public AudioSource Music;

        public int Pos;
        public float FirstBeatDelay;

        bool StartCall;
        bool shouldMove;

        void Start()
        {
            Music = GetComponent<AudioSource>();

            // Scriptable object stuff imported here
            Music.clip = SongToPlay;
            Song_BPM = 160f;


            SecPerBeat = 60f / Song_BPM;
            DSPSongTime = (float)AudioSettings.dspTime;
            Music.Play();
        }


        void Update()
        {
            Song_Pos = (float)AudioSettings.dspTime - DSPSongTime;
            Song_Pos_Beats = Song_Pos / SecPerBeat - FirstBeatDelay;

            if ((Mathf.FloorToInt(Song_Pos_Beats) > Pos) && (!StartCall))
            {
                ++Pos;
                StartCall = true;
            }
            else if (Mathf.FloorToInt(Song_Pos_Beats) > Pos)
            {
                ++Pos;
            }
        }
    }
}