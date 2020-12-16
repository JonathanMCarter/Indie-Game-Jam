using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class RotateScript : MonoBehaviour
    {
        [SerializeField] private bool xAxis = false;      // Bool for the XAxis
        [SerializeField] private bool yAxis = false;      // Bool for the YAxis
        [SerializeField] private bool zAxis = false;      // Bool for the ZAxis
        [SerializeField] private float speed = 1;         // Float for the speed of the rotation

        private bool shouldRotateObject = true;           // boolean that enables or disables the rotation of an object (also sets it to true as default)


        private void FixedUpdate()
        {
            if (shouldRotateObject)
            {
                // Roates the object with whatever rotation selected at the desired speed (note there is not time.deltatime here so its small changes
                transform.Rotate(ConvertBool(xAxis) * speed, ConvertBool(yAxis) * speed, ConvertBool(zAxis) * speed);
            }
        }


        /// <summary>
        /// Function to convert boolean to int for use in the ratation
        /// </summary>
        /// <param name="input">the inputted boolean value</param>
        /// <returns>an int value for the inputted boolean</returns>
        private int ConvertBool(bool input)
        {
            int convert;
            convert = input ? 1 : 0;
            return (convert);
        }


        /// <summary>
        /// Enables the rotation
        /// </summary>
        public void EnableRotation()
        {
            if (!shouldRotateObject)
            {
                shouldRotateObject = true;
            }
        }


        /// <summary>
        /// Disables the rotation
        /// </summary>
        public void DisableRotation()
        {
            if (shouldRotateObject)
            {
                shouldRotateObject = false;
            }
        }
    }
}