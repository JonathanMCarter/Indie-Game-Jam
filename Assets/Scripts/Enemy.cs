using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MusicalTurnBased
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject player;

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
    }
}