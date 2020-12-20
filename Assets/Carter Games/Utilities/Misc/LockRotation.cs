using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Lock Rotation
 *	    Forces a rotation to stay the same constantly.
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
    /// Class | Locks the rotation of an object via code.
    /// </summary>
    public class LockRotation : MonoBehaviour
    {
        /// <summary>
        /// Bool | should the script use local rotation?
        /// </summary>
        [Header("Local or World Space?")]
        [SerializeField] private bool useLocalRot;

        /// <summary>
        /// Bool | should the script use late update?
        /// </summary>
        [Header("Late or standard Update Method?")]
        [SerializeField] private bool useLateUpdate;

        /// <summary>
        /// Quaternion | the start rotation, set in start method.
        /// </summary>
        private Quaternion startRot;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start | Sets the start rotation based on whther the local rotation or standard rotation should be used.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            if (!useLocalRot)
                startRot = transform.rotation;
            else
                startRot = transform.localRotation;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update | Sets to rotation to the start rotation if it is not currently the start rotation. Only runs if it is set to not use Late Update.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (!useLateUpdate)
            {
                if (!useLocalRot && !transform.rotation.Equals(startRot))
                    transform.rotation = startRot;
                else if (!transform.localRotation.Equals(startRot))
                    transform.localRotation = startRot;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Late Update | Sets to rotation to the start rotation if it is not currently the start rotation. Only runs if it is set to not use Update.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void LateUpdate()
        {
            if (useLateUpdate)
            {
                if (!useLocalRot && !transform.rotation.Equals(startRot))
                    transform.rotation = startRot;
                else if (!transform.localRotation.Equals(startRot))
                    transform.localRotation = startRot;
            }
        }
    }
}