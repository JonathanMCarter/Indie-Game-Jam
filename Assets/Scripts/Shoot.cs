using UnityEngine;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private float weaponSpd;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private GameObject[] weaponPool;
        [SerializeField] private int poolLength;


        private void Start()
        {
            weaponPool = new GameObject[poolLength];

            for (int i = 0; i < poolLength; i++)
            {
                GameObject _go = Instantiate(weaponPrefab);
                _go.SetActive(false);
                weaponPool[i] = _go;
            }
        }


        public void MakeAction()
        {
            for (int i = 0; i < poolLength; i++)
            {
                if (!weaponPool[i].activeInHierarchy)
                {
                    weaponPool[i].GetComponent<Rigidbody>().velocity = transform.GetChild(0).transform.GetChild(0).transform.forward * weaponSpd * Time.deltaTime;
                    weaponPool[i].SetActive(true);
                }
            }
        }
    }
}