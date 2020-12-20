using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Enable / Disable
 *	    Enables or disables the elements in the defined array.
 *	    
 *	Requirements:
 *	    - an instance of the UI Button Switch class attached to the same GameObject.
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
    /// Class | UI Button Switch Scaling Effect, runs an enable/disable on the objects based on their current status.
    /// </summary>
    public class UIBSEnableDisable : MonoBehaviour
    {
        /// <summary>
        /// Bool | Defines if the effect should happen or not.
        /// </summary>
        [Header("Enable/Disable Settings")]
        [Tooltip("Controls if the effect should happen.")]
        [SerializeField] private bool shouldEnableDisable;

        /// <summary>
        /// GameObject Array | all elements to effect.
        /// </summary>
        [Tooltip("Defines what objects are toggled by this effect.")]
        [SerializeField] private GameObject[] toEnableDisable;

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
        /// Method | Runs the enable / disable effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void EnableDisable()
        {
            if (shouldEnableDisable)
            {
                for (int i = 0; i < toEnableDisable.Length; i++)
                {
                    if (!i.Equals(uibs.pos))
                    {
                        toEnableDisable[i].SetActive(false);
                    }
                    else
                    {
                        toEnableDisable[i].SetActive(true);
                    }
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Reverts the effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void RevertEffect()
        {
            for (int i = 0; i < toEnableDisable.Length; i++)
            {
                toEnableDisable[i].SetActive(true);
            }
        }
    }
}