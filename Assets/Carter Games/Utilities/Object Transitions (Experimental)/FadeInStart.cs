using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class FadeInStart : MonoBehaviour
    {
        [SerializeField] private float multiplier = 1f;
        [SerializeField] private Vector3 targetScale = Vector3.one;
        private bool isSetup;


        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            isSetup = false;
        }


        private void Update()
        {
            if (!isSetup)
            {
                if (transform.localScale.x < targetScale.x)
                {
                    transform.localScale += targetScale * multiplier * Time.deltaTime;
                }
                else
                {
                    transform.localScale = targetScale;
                    isSetup = true;
                    enabled = false;
                }
            }
        }
    }
}