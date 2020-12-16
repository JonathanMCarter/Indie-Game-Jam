using UnityEngine;
using UnityEngine.UI;

/************************************************************************************
 * 
 *							        Leaderboard Manager
 *							  
 *				                   ** Example Script **
 *	   This is not needed for the asset to work, it is just used in the example scene.
 * Please do not edit this code as it will break the example scene provided with this asset.
 *			
 *			                        Script written by: 
 *			           Jonathan Carter (https://jonathan.carter.games)
 *			        
 *									Version: 1.0.2
 *						   Last Updated: 07/10/2020 (d/m/y)					
 * 
*************************************************************************************/

namespace CarterGames.Assets.LeaderboardManager.Example
{
    /// <summary>
    /// (CLASS) - Example class used in the Leaderboard Manager Example Scene to showcase the asset's functionality. 
    /// </summary>
    public class LeaderboardManagerExample : MonoBehaviour
    {
        /// <summary>
        /// The player name input field text component.
        /// </summary>
        [Header("Example Text Fields")]
        [Tooltip("The text field for the player name input")]
        public Text playerName;

        /// <summary>
        /// The player score input field text component.
        /// </summary>
        [Tooltip("The text field for the player score input")]
        public Text playerScore;

        /// <summary>
        /// Reference to the display script for a for of the methods in it.
        /// </summary>
        [Header("Display Script Reference")]
        [Tooltip("a reference to the display script on a gameObject in the scene somewhere.")]
        public LeaderboardDisplay displayScript;

        /// <summary>
        /// Calls the add to leaderboard method and passes through the values from the input fields.
        /// (NOTE: float.phase() is used here as we are getting a text component, normally you'd have the float value from the game's score to use instead)
        /// </summary>
        public void AddToLB()
        {
            LeaderboardManager.AddToLeaderboard(playerName.text, float.Parse(playerScore.text));
        }

        /// <summary>
        /// Calls the remove from leaderboard method and passes through the values from the input field.
        /// </summary>
        public void RemoveFromLB()
        {
            LeaderboardManager.RemoveEntryFromLeaderboard(playerName.text, float.Parse(playerScore.text));
        }

        /// <summary>
        /// Calls the update leaderboard method on the leaderboard display script.
        /// Needs a reference the leaderboard display script to work.
        /// </summary>
        public void UpdateLBDisplay()
        {
            displayScript.UpdateLeaderboardDisplay();
        }


        /// <summary>
        /// Calls the clear leaderboard data method from the leaderboard manager.
        /// </summary>
        public void ClearLBData()
        {
            LeaderboardManager.ClearLeaderboardData();
        }


        /// <summary>
        /// Calls the clear leaderboard method on the display script.
        /// </summary>
        public void ClearLBDisplay()
        {
            displayScript.ClearLeaderboard();
        }
    }
}