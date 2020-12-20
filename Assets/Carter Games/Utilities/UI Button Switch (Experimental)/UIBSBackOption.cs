using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Back Option
 *	    Provides the option to have an event run on back option.
 *	    
 *	Requirements:
 *	    - an instance of the UI Button Switch class attached to the same GameObject.
 *	    - InputActions class "Actions"
 *	    - Input Map called "Back" configured to be a button for controllers.
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
    /// Class | Controls the back action on a menu item.
    /// </summary>
    public class UIBSBackOption : MonoBehaviour
    {
        /// <summary>
        /// The events that should run on the "back" press.
        /// </summary>
        [SerializeField] private UnityEvent backAction;

        /// <summary>
        /// UI Button Switch | Reference to the UI button switch script.
        /// </summary>
        private UIButtonSwitch uibs;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Only refers to the UIBS class.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update | Runs the event when called.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (uibs.enabled && uibs.action != null && !uibs.isCoR)
            {
                if (uibs.action.Menu.Cancel.phase.Equals(InputActionPhase.Performed))
                {
                    backAction.Invoke();
                }
            }
        }
    }
}