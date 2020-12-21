using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class FadeOutOnEnd : MonoBehaviour
    {
        [SerializeField] private bool runEffect;
        [SerializeField] private float multiplier = 1f;
        private bool hasCompleted;


        private void Update()
        {
            if (runEffect)
            {
                FadeOut();
            }
        }


        public void PerformEndEffect()
        {
            runEffect = true;
        }


        private void FadeOut()
        {
            if (!hasCompleted)
            {
                if (transform.localScale.x > .01f)
                {
                    transform.localScale -= Vector3.one * multiplier * Time.deltaTime;
                }
                else
                {
                    transform.localScale = Vector3.zero;
                    hasCompleted = true;
                }
            }
        }
    }
}