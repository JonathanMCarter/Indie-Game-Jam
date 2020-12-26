using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  UI Button Switch
 *	    A base class that make controller input on UI elements easier.
 *	    
 *	Requirements:
 *	    - New Unity Input System
 *	    - InputActions class "Actions" (Edit Lines: 80, 110, 113, 131, 160, 164, 171, 175, 188, 192, 199, 203, 216, 220, 225, 229, if not)
 *	    - Input Map called "Movement" of type Vector2 for controllers. (Edit Lines: 160, 164, 171, 175, 188, 192, 199, 203, 216, 220, 225, 229, if not)
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
    /// Class | the base class for the UI Button Switch scripts. Handles the main funcionaility of the system.
    /// </summary>
    public class UIButtonSwitch : MonoBehaviour
    {
        /// <summary>
        /// GameObject Array | A grouping of all the buttons in the menu to effect.
        /// </summary>
        [Header("UI Buttons")]
        [Tooltip("The buttons to effect, these should have UIBS Button Actions on them.")]
        [SerializeField] internal GameObject[] buttons;
        [Space(5f)]

        /// <summary>
        /// Bool | Defines whether or not the menu navigates up/down.
        /// </summary>
        [Tooltip("If the menu navigates Up/Down the set this to true, for Left/Right leave it false.")]
        [SerializeField] private bool isUD;

        /// <summary>
        /// Bool | Defines if the position should be reset when enabled.
        /// </summary>
        [Tooltip("Defines whether or not the position is reset when this manager is enabled.")]
        [SerializeField] private bool resetPos;
        [Space(5f)]

        /// <summary>
        /// Bool | Defines whether or not to use a grid format for the movement.
        /// </summary>
        [Tooltip("Defines whether or not to use all directions (grid format).")]
        [SerializeField] private bool useGrid;

        /// <summary>
        /// Int | Defines how many columns are in the grid format, not needed if grid format is not in use.
        /// </summary>
        [SerializeField] private int maxColumns;
        [Space(5f)]

        /// <summary>
        /// UnityEvent | All effects to run on the active option.
        /// </summary>
        [SerializeField] private UnityEvent effects;

        /// <summary>
        /// Bool | Defines if the coroutine running? 
        /// </summary>
        internal bool isCoR;

        /// <summary>
        /// Actions | The input actions to check against (Edit if called something else).
        /// </summary>
        internal Actions action;

        /// <summary>
        /// Bool | is the player using a controller?
        /// </summary>
        internal bool isUsingController;

        /// <summary>
        /// Int | The position the user is at in the menu.
        /// </summary>
        internal int pos;

        /// <summary>
        /// Int | The max position the user can be at.
        /// </summary>
        internal int maxPos;

        /// <summary>
        /// InputDevice | The variable to check if a controller is active.
        /// </summary>
        public InputDevice currentActiveDevice;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity OnEnable | Enables the input and position.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            action = new Actions();
            action.Enable();

            ForceButtonDelay();

            if (resetPos)
            {
                pos = 0;
                effects.Invoke();
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity OnDisable | Stops the input and all.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            StopAllCoroutines();
            action.Disable();
            isCoR = false;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start | Does basic setup for the position & runs the effects for the position 0 button.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            pos = 0;
            maxPos = buttons.Length - 1;
            isCoR = false;

            if (effects != null)
            {
                effects.Invoke();
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update | Handles the movement around the menu & what controller is active if there is one.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            // Active Controller if enabled.
            isUsingController = IsControllerActive();

            // The menu movement (Non grid).
            if (isUsingController)
            {
                if (!isCoR)
                {
                    if (isUD)
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                }
            }
            else
            {
                if (!isCoR)
                {
                    if (isUD)
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                }
            }


            // The menu movement (Grid)
            if (!isCoR)
            {
                if (useGrid)
                {
                    if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                    {
                        UpdatePos(-maxColumns, true);
                    }
                    else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                    {
                        UpdatePos(maxColumns, true);
                    }

                    if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                    {
                        UpdatePos(1, true);
                    }
                    else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                    {
                        UpdatePos(-1, true);
                    }
                }
            }


            // Confirm Button.
            if (!isCoR)
            {
                if (action.Menu.Accept.phase.Equals(InputActionPhase.Performed))
                {
                    if (buttons[pos].GetComponent<UIBSButtonActions>().canPerformActions)
                    {
                        buttons[pos].GetComponent<UIBSButtonActions>().action.Invoke();
                        StartCoroutine(ButtonDelay());
                    }
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Fixed Update | Used to set whether or not a controller is active.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void FixedUpdate()
        {
            InputSystem.onActionChange += (obj, change) =>
            {
                if (change == InputActionChange.ActionPerformed)
                {
                    var inputAction = (InputAction)obj;
                    var lastControl = inputAction.activeControl;
                    currentActiveDevice = lastControl.device;
                }
            };
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Updates the position in the menu.
        /// </summary>
        /// <param name="value">value to change to.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void UpdatePos(int value, bool useGrid = false)
        {
            pos += value;

            if (useGrid)
            {
                if (pos <= -1)
                {
                    pos = (pos + 1) + maxPos;
                }
                else if (pos >= (maxPos + 1))
                {
                    pos = (pos - 1) - maxPos;
                }
            }
            else
            {
                if (pos.Equals(maxPos + 1))
                {
                    pos = 0;
                }
                else if (pos.Equals(-1))
                {
                    pos = maxPos;
                }
            }

            if (effects != null)
            {
                effects.Invoke();
            }

            StartCoroutine(ButtonDelay());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Checks to see if a controller (either XB or PS is plugged in).
        /// </summary>
        /// <returns>True if there is a supported controller plugged in, false if not.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private bool IsControllerActive()
        {
            if (currentActiveDevice != null)
                if (currentActiveDevice.displayName.Contains("Xbox") || currentActiveDevice.displayName.Contains("Play"))
                    return true;
                else
                    return false;
            else
                return false;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Coroutine | Runs a short button delay on the menu buttons.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        internal IEnumerator ButtonDelay()
        {
            isCoR = true;
            yield return new WaitForSecondsRealtime(.3f);
            isCoR = false;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Forces the button delay to run.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void ForceButtonDelay()
        {
            StartCoroutine(ButtonDelay());
        }
    }
}