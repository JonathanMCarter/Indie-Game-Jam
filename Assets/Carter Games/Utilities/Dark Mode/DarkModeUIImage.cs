using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *	Dark Mode UI Image
 *	    Adds options for light/dark mode for UI Text in the game, 
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
    /// Inheriting Class | Handles Dark Mode on UI Image elements.
    /// </summary>
    public class DarkModeUIImage : DarkModeUI
    {
        /// <summary>
        /// Image | The image to edit.
        /// </summary>
        private Image img;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Runs the base class awake method as well as the needed code to reference the image itself.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private new void Awake()
        {
            // reference to image
            img = GetComponent<Image>();

            // sets alpha if it is not set already xD.
            base.Awake();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Sets the colour based on whether dark mode is on or off.
        /// </summary>
        /// <param name="isDarkMode">Determines whether or not to use. Default is false.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void SetColour(bool isDarkMode = false)
        {
            if (isDarkMode && !img.color.Equals(base.darkModeColour))
                img.color = base.darkModeColour;
            else if (!isDarkMode && !img.color.Equals(base.lightModeColour))
                img.color = base.lightModeColour;
        }
    }
}