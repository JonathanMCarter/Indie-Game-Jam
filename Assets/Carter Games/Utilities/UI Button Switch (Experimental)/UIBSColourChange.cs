using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Colour Changer
 *	    Changes the colour on the active position element.
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
    /// Class | Controls the Colour Change effect on the UIBS system.
    /// </summary>
    public class UIBSColourChange : MonoBehaviour
    {
        /// <summary>
        /// Bool | Defines if the effect should happen or not.
        /// </summary>
        [Header("Colour Change Settings")]
        [Tooltip("Controls if the effect should happen.")]
        [SerializeField] private bool shouldChangeColour;

        /// <summary>
        /// The colour the object already was.
        /// </summary>
        [SerializeField] private Color defaultColour;

        /// <summary>
        /// The colour to change to when active.
        /// </summary>
        [SerializeField] private Color hoverColour;

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
        /// Method | Changes the colour of the active object to the hover/default colour.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ChangeColour()
        {
            if (shouldChangeColour)
            {
                for (int i = 0; i < uibs.buttons.Length; i++)
                {
                    if (!i.Equals(uibs.pos))
                    {
                        uibs.buttons[i].GetComponent<Image>().color = defaultColour;
                    }
                    else
                    {
                        uibs.buttons[i].GetComponent<Image>().color = hoverColour;
                    }
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Reverts the effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void RevertEffects()
        {
            for (int i = 0; i < uibs.buttons.Length; i++)
            {
                uibs.buttons[i].GetComponent<Image>().color = hoverColour;
            }
        }
    }
}