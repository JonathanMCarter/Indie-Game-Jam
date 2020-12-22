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

        private TurnController tc;
        private GameObject toMoveTo;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
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


        private Moves ChooseDirection()
        {
            Vector3 _check = player.transform.position - transform.position;

            if (_check.x > _check.z)
            {
                if ((_check.z * -1) < _check.x)
                {
                    if (_check.x > .1f)
                    {
                        Debug.Log("Enemy - Go Left");
                        return Moves.Left;
                    }
                    else if (_check.x < -.1f)
                    {
                        Debug.Log("Enemy - Go Right");
                        return Moves.Right;
                    }
                }
                else if (_check.z < -.1f)
                {
                    Debug.Log("Enemy - Go Up");
                    return Moves.Up;
                }
            }
            else if (_check.z > _check.x)
            {
                if ((_check.x * -1) < _check.z)
                {
                    if (_check.z > .1f)
                    {
                        Debug.Log("Enemy - Go Down");
                        return Moves.Down;
                    }
                    else if (_check.z < -.1f)
                    {
                        Debug.Log("Enemy - Go Up");
                        return Moves.Up;
                    }
                }
                else if (_check.x < -.1f)
                {
                    Debug.Log("Enemy - Go Right");
                    return Moves.Right;
                }
            }

            return Moves.Defend;
        }


        private void MoveEnemy()
        {
            if (assignedMoves == null || assignedMoves.Count.Equals(0))
            {
                RaycastHit _hit;

                switch (ChooseDirection())
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
            }
            else
            {
                RaycastHit _hit;

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