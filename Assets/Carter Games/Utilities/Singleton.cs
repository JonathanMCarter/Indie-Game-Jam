using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class Singleton : MonoBehaviour
    {
        public string singletonID;
        public int id;

        private Singleton[] objects;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            objects = FindObjectsOfType<Singleton>();
            id = FindObjectsOfType<Singleton>().Length;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].singletonID.Equals(this.singletonID))
                {
                    if (!objects[i].id.Equals(1))
                    {
                        Destroy(gameObject, .1f);
                    }
                }
            }
        }
    }
}