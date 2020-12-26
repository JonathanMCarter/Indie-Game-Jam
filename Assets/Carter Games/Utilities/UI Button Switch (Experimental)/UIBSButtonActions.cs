using UnityEngine;
using UnityEngine.Events;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Button Actions
 *	    Allows any object to perform actions when the UIBS button is pressed.
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
    /// Class | Controls the actions that a button does when confirmed.
    /// </summary>
    public class UIBSButtonActions : MonoBehaviour
    {
        /// <summary>
        /// Bool | Defines whether or not the actions should be run, helpful if some buttons need unlocking to work.
        /// Added in No Presents For You
        /// </summary>
        [SerializeField] internal bool canPerformActions = true;

        /// <summary>
        /// UnityEvent | The actions to perfrom when "confirm" is pressed.
        /// </summary>
        [Header("Actions to perform.")]
        [Tooltip("A grouping of events to run on confirm.")]
        public UnityEvent action;
    }
}