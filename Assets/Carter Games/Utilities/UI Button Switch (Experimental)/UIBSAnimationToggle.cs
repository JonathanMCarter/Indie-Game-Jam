using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch Animation Toggle
 *	    Toggles an animation when the UIBS is in the correct position.
 *	    
 *	Requirements:
 *	    - an instance of the UI Button Switch class attached to the same GameObject.
 *	    - animator triggers called "Open" & "Close".
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
    public class UIBSAnimationToggle : MonoBehaviour
    {
        /// <summary>
        /// Animator Array | all the animators for run on.
        /// </summary>
        [Header("Animation Toggle Settings")]
        [SerializeField] private Animator[] anims;


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
        /// Method | Toggles the animation either on or off based on its current state.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AnimationToggle()
        {
            for (int i = 0; i < anims.Length; i++)
            {
                if (i.Equals(uibs.pos))
                {
                    anims[i].ResetTrigger("Close");
                    anims[i].SetTrigger("Open");
                }
                else
                {
                    anims[i].ResetTrigger("Open");
                    anims[i].SetTrigger("Close");
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
            for (int i = 0; i < anims.Length; i++)
            {
                anims[i].ResetTrigger("Open");
                anims[i].SetTrigger("Close");
            }
        }
    }
}