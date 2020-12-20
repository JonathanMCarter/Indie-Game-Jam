using UnityEngine;
using UnityEngine.UI;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *	Dark Mode UI
 *	    Base class that handles most of the Dark Mode UI system. 
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
    /// Base Class | Darm Mode UI, handles the base requirements for the dark mode UI system.
    /// </summary>
    public class DarkModeUI : MonoBehaviour
    {
        /// <summary>
        /// Color32 | The light mode colour for this object.
        /// </summary>
        [Tooltip("The colour that will be set when in light mode (alpha does not need setting)")]
        [SerializeField] internal Color32 lightModeColour;

        /// <summary>
        /// Color32 | The dark mode colour for this object.
        /// </summary>
        [Tooltip("The colour that will be set when in dark mode (alpha does not need setting)")]
        [SerializeField] internal Color32 darkModeColour;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake | Can be overridden by inheriting classes to add their own additions in Awake while still running this code.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public virtual void Awake()
        {
            // sets alpha if it is not set already xD. 
            // saves time to not have to set the alpha of the colour in the inspector.
            lightModeColour = AddAlphaToColour(lightModeColour);
            darkModeColour = AddAlphaToColour(darkModeColour);

            // Calls the dark mode update method.
            UpdateDarkModeUI();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Overriding Method | Sets the colour based on whether dark mode is on or off.
        /// </summary>
        /// <param name="isDarkMode">Determines whether or not to use. Default is false.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public virtual void SetColour(bool isDarkMode = false)
        {
            // Override in children to do functionality, blank for this class as there is nothing to set xD.
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Adds alpha to a colour if it is set to 0.
        /// </summary>
        /// <param name="_col">Color32 to edit.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private Color32 AddAlphaToColour(Color32 _col)
        {
            if (_col.a.Equals(0))
                return new Color32(_col.r, _col.g, _col.b, 255);
            else
                return _col;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Updates the scene to view the dark mode if the player perf has changed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void UpdateDarkModeUI()
        {
            // edits the mode based on the perf.
            if (PlayerPrefs.HasKey("CG-U-DarkMode"))
            {
                if (PlayerPrefs.GetInt("CG-U-DarkMode").Equals(1))
                    SetColour(true);
                else
                    SetColour();
            }
        }
    }
}