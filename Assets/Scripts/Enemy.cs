using UnityEngine;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.NoPresentsForYou
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private List<Moves> assignedMoves;
        [SerializeField] private Moves[] pathFinding;

        private TurnController tc;
        private GameObject toMoveTo;
        private RaycastHit _hit;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
            pathFinding = new Moves[4];
        }


        private void Update()
        {
            ChooseDirection();

            if (!tc.movesThisTurn.Contains(this.gameObject))
                tc.AddMove(this.gameObject);

            if (toMoveTo)
            {
                transform.position = Vector3.Lerp(transform.position, toMoveTo.transform.GetChild(0).transform.position, 4 * Time.deltaTime);
            }
        }


        public void MakeMove()
        {
            MoveEnemy();
        }


        private char IsPositive(float value)
        {
            if (value > 0)
                return 'p';
            else if (value < 0)
                return 'n';
            else
                return 'z';
        }

        /// <summary>
        /// Legit using my Y1-PF-AE2 Pathfinding Solution, as it still works and my pervious attempt had a few troubles with walls/holes.
        /// </summary>
        /// <returns></returns>
        private void ChooseDirection()
        {
            Vector3 _check = player.transform.position - transform.position;
            bool isZ = false;


            if (_check.z > _check.x)
                isZ = true;


            float _testX = IsPositive(_check.x);
            float _testZ = IsPositive(_check.z);


            if (isZ)
            {
                switch (_testZ)
                {
                    case 'p':
                        pathFinding[0] = Moves.Down;
                        pathFinding[3] = Moves.Up;
                        break;
                    case 'n':
                        pathFinding[0] = Moves.Up;
                        pathFinding[3] = Moves.Down;
                        break;
                    case 'z':
                        pathFinding[0] = Moves.Down;
                        pathFinding[3] = Moves.Up;
                        break;
                    default:
                        break;
                }

                switch (_testX)
                {
                    case 'p':
                        pathFinding[1] = Moves.Left;
                        pathFinding[2] = Moves.Right;
                        break;
                    case 'n':
                        pathFinding[1] = Moves.Right;
                        pathFinding[2] = Moves.Left;
                        break;
                    case 'z':
                        pathFinding[1] = Moves.Left;
                        pathFinding[2] = Moves.Right;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_testX)
                {
                    case 'p':
                        pathFinding[0] = Moves.Left;
                        pathFinding[3] = Moves.Right;
                        break;
                    case 'n':
                        pathFinding[0] = Moves.Right;
                        pathFinding[3] = Moves.Left;
                        break;
                    case 'z':
                        pathFinding[0] = Moves.Left;
                        pathFinding[3] = Moves.Right;
                        break;
                    default:
                        break;
                }

                switch (_testZ)
                {
                    case 'p':
                        pathFinding[1] = Moves.Down;
                        pathFinding[2] = Moves.Up;
                        break;
                    case 'n':
                        pathFinding[1] = Moves.Up;
                        pathFinding[2] = Moves.Down;
                        break;
                    case 'z':
                        pathFinding[1] = Moves.Down;
                        pathFinding[2] = Moves.Up;
                        break;
                    default:
                        break;
                }
            }
        }



        private bool CheckDirection(Moves toCheck)
        {
            switch (toCheck)
            {
                case Moves.Up:
                    if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                case Moves.Down:
                    if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                case Moves.Left:
                    if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                case Moves.Right:
                    if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                    {
                        if (_hit.collider.CompareTag("Tile"))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                default:
                    break;
            }

            return false;
        }


        private void MoveEnemy()
        {
            if (assignedMoves == null || assignedMoves.Count.Equals(0))
            {
                if (CheckDirection(pathFinding[0]))
                {
                    switch (pathFinding[0])
                    {
                        case Moves.Up:
                            if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
                            }
                            break;
                        case Moves.Down:
                            if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Left:
                            if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Right:
                            if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                    }

                }
                else if (CheckDirection(pathFinding[1]))
                {
                    switch (pathFinding[1])
                    {
                        case Moves.Up:
                            if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
                            }
                            break;
                        case Moves.Down:
                            if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Left:
                            if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Right:
                            if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                    }
                }
                else if (CheckDirection(pathFinding[2]))
                {
                    switch (pathFinding[2])
                    {
                        case Moves.Up:
                            if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
                            }
                            break;
                        case Moves.Down:
                            if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Left:
                            if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Right:
                            if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                    }
                }
                else if (CheckDirection(pathFinding[4]))
                {
                    switch (pathFinding[4])
                    {
                        case Moves.Up:
                            if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
                            }
                            break;
                        case Moves.Down:
                            if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Left:
                            if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case Moves.Right:
                            if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                            {
                                toMoveTo = _hit.collider.gameObject;
                                transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                    }
                }
            }
            else
            {
                if (assignedMoves.Count > 0)
                {
                    switch (assignedMoves[assignedMoves.Count - 1])
                    {
                        case Moves.Up:

                            if (Physics.Raycast(transform.position, -transform.forward * 2f, out _hit))
                            {
                                if (_hit.collider.CompareTag("Tile"))
                                {
                                    toMoveTo = _hit.collider.gameObject;
                                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 180, 0);
                                }
                            }

                            break;
                        case Moves.Down:

                            if (Physics.Raycast(transform.position, transform.forward * 2f, out _hit))
                            {
                                if (_hit.collider.CompareTag("Tile"))
                                {
                                    toMoveTo = _hit.collider.gameObject;
                                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                                }
                            }

                            break;
                        case Moves.Left:

                            if (Physics.Raycast(transform.position, transform.right * 2f, out _hit))
                            {
                                if (_hit.collider.CompareTag("Tile"))
                                {
                                    toMoveTo = _hit.collider.gameObject;
                                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 90, 0);
                                }
                            }

                            break;
                        case Moves.Right:

                            if (Physics.Raycast(transform.position, -transform.right * 2f, out _hit))
                            {
                                if (_hit.collider.CompareTag("Tile"))
                                {
                                    toMoveTo = _hit.collider.gameObject;
                                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, -90, 0);
                                }
                            }

                            break;
                        default:
                            break;
                    }

                    assignedMoves.RemoveAt(assignedMoves.Count - 1);
                }
            }
        }
    }
}