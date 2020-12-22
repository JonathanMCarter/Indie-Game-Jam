using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Moves selectedMove;
        [SerializeField] private Image[] movementArrows;

        private Actions actions;
        private TurnController tc;
        private GameObject toMoveTo;


        private void OnEnable()
        {
            actions = new Actions();
            actions.Enable();
        }


        private void OnDisable()
        {
            actions.Disable();
        }


        private void Start()
        {
            tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
        }


        private void Update()
        {
            ReadInput();

            if (toMoveTo)
            {
                transform.position = Vector3.Lerp(transform.position, toMoveTo.transform.GetChild(0).transform.position, 4 * Time.deltaTime);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Em"))
            {
                gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneChanger>().LevelFailed();
            }
        }


        private void ReadInput()
        {
            if (actions.Gameplay.Movement.ReadValue<Vector2>().x > .1f)
            {
                SendMove(Moves.Right);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (actions.Gameplay.Movement.ReadValue<Vector2>().x < -.1f)
            {
                SendMove(Moves.Left);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (actions.Gameplay.Movement.ReadValue<Vector2>().y > .1f)
            {
                SendMove(Moves.Up);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (actions.Gameplay.Movement.ReadValue<Vector2>().y < -.1f)
            {
                SendMove(Moves.Down);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        public void SendMove(Moves _move)
        {
            selectedMove = _move;
            tc.playerAction = this.gameObject;
            SetMovementArrow();
        }


        public void MakeMove()
        {
            switch (selectedMove)
            {
                case Moves.Up:
                    MovePlayer();
                    Debug.Log("Move-Up");
                    break;
                case Moves.Down:
                    MovePlayer();
                    Debug.Log("Move-Down");
                    break;
                case Moves.Left:
                    MovePlayer();
                    Debug.Log("Move-Left");
                    break;
                case Moves.Right:
                    MovePlayer();
                    Debug.Log("Move-Right");
                    break;
                case Moves.Attack:
                    Debug.Log("Attack");
                    break;
                case Moves.Defend:
                    Debug.Log("Defend");
                    break;
                case Moves.Steal:
                    Debug.Log("Steal");
                    break;
                case Moves.Leave:
                    Debug.Log("Leave");
                    break;
                default:
                    break;
            }
        }


        private void SetMovementArrow()
        {
            if ((int)selectedMove <= 3)
            {
                for (int i = 0; i < movementArrows.Length; i++)
                {
                    if (movementArrows[i].enabled)
                        movementArrows[i].enabled = false;
                }

                if (!movementArrows[(int)selectedMove].enabled)
                    movementArrows[(int)selectedMove].enabled = true;

            }
        }


        private void MovePlayer()
        {
            RaycastHit _hit;

            switch (selectedMove)
            {
                case Moves.Up:

                    if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                        {
                            toMoveTo = _hit.collider.gameObject;
                        }
                    }

                    break;
                case Moves.Down:

                    if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                        {
                            toMoveTo = _hit.collider.gameObject;
                        }
                    }

                    break;
                case Moves.Left:

                    if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                        {
                            toMoveTo = _hit.collider.gameObject;
                        }
                    }

                    break;
                case Moves.Right:

                    if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                        {
                            toMoveTo = _hit.collider.gameObject;
                        }
                    }

                    break;
                default:
                    break;
            }

            for (int i = 0; i < movementArrows.Length; i++)
            {
                if (movementArrows[i].enabled)
                    movementArrows[i].enabled = false;
            }
        }
    }
}