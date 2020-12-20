using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *	Dark Mode UI Text
 *	    Adds options for light/dark mode for UI Text in the game.
 *	    
 *	Inherits From:
 *	    Dark Mode UI
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
    /// Inheriting Class | Handles Dark Mode on UI Text elements.
    /// </summary>
    public class DarkModeUIText : DarkModeUI
    {
        /// <summary>
        /// Text | The text element that is to be edited
        /// </summary>
        private Text txt;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Runs the base class awake method as well as the needed code to reference the text element itself.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private new void Awake()
        {
            // reference to text
            txt = GetComponent<Text>();

            // sets alpha if it is not set already xD.
            base.Awake();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Overridable Method | Sets the colour based on whether dark mode is on or off.
        /// </summary>
        /// <param name="isDarkMode">Determines whether or not to use. Default is false.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void SetColour(bool isDarkMode = false)
        {
            if (isDarkMode && !txt.color.Equals(base.darkModeColour))
                txt.color = base.darkModeColour;
            else if (!isDarkMode && !txt.color.Equals(base.lightModeColour))
               txt.color = base.lightModeColour;
        }
    }
}