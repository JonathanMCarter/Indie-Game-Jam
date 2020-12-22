using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class IntroController : MonoBehaviour
    {
        [SerializeField] private UnityEvent[] events;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            //StartCoroutine()
        }


        private IEnumerator Delay(UnityEvent _event, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _event.Invoke();
        }
    }
}