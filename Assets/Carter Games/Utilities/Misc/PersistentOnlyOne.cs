using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Persistent Only One
 *	    Forces a rotation to stay the same constantly.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *			        
 *	Last Updated: 18/12/2020 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Utilities
{
    /// <summary>
    /// Class | Forces the object attached to persist between scenes and remove duplicates that have this script on them.
    /// </summary>
    public class PersistentOnlyOne : MonoBehaviour
    {
        /// <summary>
        /// String | The ID to check against when removing duplicates of this script (so it doesn't remove different persistent objects).
        /// </summary>
        [Header("Grouping ID")]
        [Tooltip("The string to match on any duplicates of this class.")]
        public string singletonID;

        /// <summary>
        /// Int | The ID for the instance of the script, used internally to make sure therie is only 1 of the script with the grouping ID in question.
        /// </summary>
        private int id;

        /// <summary>
        /// PersistentOnlyOne | an array of all objects with this script to check through.
        /// </summary>
        private PersistentOnlyOne[] objects;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Removes all the duplicates with the same grouping id.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            objects = FindObjectsOfType<PersistentOnlyOne>();
            id = FindObjectsOfType<PersistentOnlyOne>().Length;

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].singletonID.Equals(this.singletonID))
                {
                    if (!objects[i].id.Equals(1))
                    {
                        Destroy(gameObject, .01f);
                    }
                }
            }
        }
    }
}