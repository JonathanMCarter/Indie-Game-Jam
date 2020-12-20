using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Mobile Element
 *	    Make GameObjects only enable or disable if the platform is mobile. Handy multi platform games that have elements for mobile only.
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
    /// Class | Makes the object attached to a element that is only on android or IOS
    /// </summary>
    public class MobileElement : MonoBehaviour
    {
        /// <summary>
        /// Enum | Effects that can be run on the object.
        /// </summary>
        public enum MobileElementActions { SetActive, EnableButton };

        /// <summary>
        /// MobileElementActions | The action that will happen on this object.
        /// </summary>
        [Tooltip("The action to perform on this object.")]
        public MobileElementActions elementActions;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Runs the desired effect for disabling the element on non mobile devices.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            switch (elementActions)
            {
                case MobileElementActions.SetActive:
#if UNITY_ANDROID || UNITY_IOS
                    gameObject.SetActive(true);
#else
                    gameObject.SetActive(false);
#endif
                    break;
                case MobileElementActions.EnableButton:
#if UNITY_ANDROID || UNITY_IOS
                    GetComponent<Button>().interactable = true;
#else
                    GetComponent<Button>().interactable = false;
#endif
                    break;
                default:
                    break;
            }
        }
    }
}