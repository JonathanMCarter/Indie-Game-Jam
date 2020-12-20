using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Scaling Effect
 *	    Scales the UI to the defined value.
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
    /// Class | UI Button Switch Scaling Effect, runs a scalign effect when "hovering" over the UI button.
    /// </summary>
    public class UIBSScalingEffect : MonoBehaviour
    {
        /// <summary>
        /// Bool | Should the scaling effect happen?
        /// </summary>
        [Header("Scaling Settings")]
        [Tooltip("Defines whether or not effect should happen even if in the effects event.")]
        [SerializeField] private bool shouldScaleOnHover;

        /// <summary>
        /// Float | Defines how much the element scales by.
        /// </summary>
        [Tooltip("Defines how much to scale by.")]
        [SerializeField] private float scaleFactor;

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
        /// Method | Controls the hover effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void HoverButton()
        {
            if (shouldScaleOnHover)
            {
                for (int i = 0; i < uibs.buttons.Length; i++)
                {
                    if (!i.Equals(uibs.pos))
                    {
                        uibs.buttons[i].transform.localScale = Vector3.one;
                    }
                    else
                    {
                        uibs.buttons[i].transform.localScale = Vector3.one * scaleFactor;
                    }
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Reverts the scaling effect, so sets the scale to V3-1 again.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void RevertEffect()
        {
            for (int i = 0; i < uibs.buttons.Length; i++)
            {
                uibs.buttons[i].transform.localScale = Vector3.one;
            }
        }
    }
}