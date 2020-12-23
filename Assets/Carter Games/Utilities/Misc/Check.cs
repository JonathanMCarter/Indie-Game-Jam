using System.Collections.Generic;
using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *	Check
 *      Adds a load of extra checks that would normally take up alot of space in the script. 
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
    /// Static Class | Check, several checks that take a lot of lines, condensed to save space.
    /// </summary>
    public static class Check
    {
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see if the desired string is in the array of strings provided
        /// Added In: The Isolation of Isabelle.
        /// </summary>
        /// <param name="toFind">string to search for</param>
        /// <param name="strings">strings to look through</param>
        /// <returns>true or false</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool StringInArray(string toFind, string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Equals(toFind))
                {
                    return true;
                }
            }

            return false;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see if the current position is within a threashold of the target position
        /// Added In: The Isolation of Isabelle.
        /// </summary>
        /// <param name="threashold">how much off can the vector3 be?</param>
        /// <param name="target">the target Vector3</param>
        /// <param name="current">the current Vector3</param>
        /// <param name="ignoreYAxis">should the Y axis be checked?</param>
        /// <returns>true or false</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool WithinThreashold(float threashold, Vector3 target, Vector3 current, bool ignoreYAxis = true)
        {
            if (ignoreYAxis)
            {
                if (
                    current.x + threashold > target.x - threashold &&
                    current.x - threashold < target.x + threashold &&
                    current.z + threashold > target.z - threashold &&
                    current.z - threashold < target.z + threashold
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (
                    current.x + threashold > target.x - threashold &&
                    current.x - threashold < target.x + threashold &&
                    current.y + threashold > target.y - threashold &&
                    current.y - threashold < target.y + threashold &&
                    current.z + threashold > target.z - threashold &&
                    current.z - threashold < target.z + threashold
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see if a layer is present in the hits of a raycast, true if there is, false if not
        /// Added In: The Isolation of Isabelle.
        /// </summary>
        /// <param name="hits">Raycasthits to check.</param>
        /// <param name="layerToCheck">layer int to find</param>
        /// <returns>true or false</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool RaycastLayerCheck(List<UnityEngine.EventSystems.RaycastResult> hits, int layerToCheck)
        {
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i].gameObject.layer.Equals(layerToCheck))
                {
                    return true;
                }
            }

            return false;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks the two inputted Color32's to see if they are the same.
        /// Added In: Crushing!
        /// </summary>
        /// <param name="colourA">First colour to check</param>
        /// <param name="colourB">Second colour to check</param>
        /// <returns>True if the colours are the same, false if not.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool Color32Check(Color32 colourA, Color32 colourB)
        {
            if ((colourA.r == colourB.r) && (colourA.g == colourB.g) && (colourA.b == colourB.b) && (colourA.a == colourB.a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see if a colour matches a float array of 4 elements.
        /// Added In: Crushing!
        /// </summary>
        /// <param name="col">The colour to check.</param>
        /// <param name="array">The float array of 4 to check.</param>
        /// <returns>True if the colour matches the float array elements, fase if not.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool ColorVsFloatArrayCheck(Color col, float[] array)
        {
            if ((col.r == array[0]) && (col.g == array[1]) && (col.b == array[2]) && (col.a == array[3]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see which is the two values is higher, regardless of +/-.
        /// Added In: No Present For You.
        /// </summary>
        /// <param name="value0">Value 0 to check</param>
        /// <param name="value1">Value 1 to check</param>
        /// <returns>Ture if 0 is higher than 1, false otherwise.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool FaceValueCheck(float value0, float value1)
        {
            float _check0, _check1;

            if (value0 < 0)
                _check0 = value0 * -1;
            else
                _check0 = value0;

            if (value1 < 0)
                _check1 = value1 * -1;
            else
                _check1 = value1;


            if (_check0 > _check1)
                return true;
            else
                return false;
        }
    }
}