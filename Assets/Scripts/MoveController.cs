﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Passenger's Attributes")]

    public float speed = 5f;
    public float sprint = 15f;
    public Transform MovePoint;

    [Header("Passenger's State")]
    // These should stay public -> being used from another script (PassArray)
    public bool isControlled = false;
    public GameObject Hora;
    public GameObject HoraRegular;

    public bool isSeated = false;

    //definition of state machine
    private enum State { moving, seating, seated };
    private State state;

    [Header("Seats Attributes")]
    private List<Transform> Rows = new List<Transform>(5);
    private int _selectedRow = 2;
    private int _selectedColumn = 0;
    private float xToSnap = 0;

    public bool lastOne = false;

    private bool _blockMoveUp = false;
    private bool _blockMoveDown = false;
    public GameObject m_ObjectCollider;

    private GameObject _seatTaken;
    public bool isAngry = false;






    bool isThisTaken = false;

    Transform initialPos;


    Transform newPlace;

    void Start()
    {
        GameObject controller = GameObject.Find("Controller");
        controller.GetComponent<gameController>().HasStarted = true;
        Rows.Insert(0, controller.GetComponent<gameController>().Row1);
        Rows.Insert(1, controller.GetComponent<gameController>().Row2);
        Rows.Insert(2, controller.GetComponent<gameController>().Aisle);
        Rows.Insert(3, controller.GetComponent<gameController>().Row3);
        Rows.Insert(4, controller.GetComponent<gameController>().Row4);

        state = State.moving;
        bool _hasFinished = controller.GetComponent<gameController>().HasFinished;


    }

    //This is checking the trigger collision, if the object that we are colliding with has the tag "Passenger" AND he is seated (by checking IsSeated) - it blocks the player's movement
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Passenger"))
        {
            if(coll.GetComponent<MoveController>()._selectedRow < 2)
            {
                this._blockMoveUp = true;
            }
            else if (coll.GetComponent<MoveController>()._selectedRow > 2)
            {
                this._blockMoveDown = true;
                //Debug.Log("Blocked down");
            }
        }

        if (coll.gameObject.CompareTag("Death"))
        {
            isSeated = true;
            state = State.seated;
            isControlled = false;
            Invoke("Destroy", 1f);
        }

        if (coll.gameObject.CompareTag("Seat"))
        {
            xToSnap = coll.gameObject.GetComponentInParent<Transform>().transform.position.x;
            _selectedColumn++;
            _seatTaken = coll.gameObject.GetComponent<SeatLogic>().passanger_Seating;
        }

    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Passenger"))
        {
            if (coll.GetComponent<MoveController>()._selectedRow < 2)
            {
                this._blockMoveUp = true;
            }
            else if (coll.GetComponent<MoveController>()._selectedRow > 2)
            {
                this._blockMoveDown = true;
                //Debug.Log("Blocked down");
            }
            
            if (coll.GetComponent<MoveController>()._selectedRow == 1 || coll.GetComponent<MoveController>()._selectedRow == 3)
            {
                if (state == State.seated && coll.gameObject.GetComponent<MoveController>().state == State.seated)
                {
                    
                    if((coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "policeman" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "gangsta")
                        || (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "gangsta" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "policeman"))
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<PassengerFactory>().angry;
                        coll.gameObject.GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<PassengerFactory>().angry;
                        isAngry = true;
                        coll.gameObject.GetComponent<MoveController>().isAngry = true;

                    }
                    if ((coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "woman" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "rabbi")
                        || (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "rabbi" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "woman"))
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<PassengerFactory>().angry;
                        coll.gameObject.GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<PassengerFactory>().angry;
                        isAngry = true;
                        coll.gameObject.GetComponent<MoveController>().isAngry = true;

                    }
                    if ((coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "grandpa" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "baby")
                        || (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "baby" && this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "grandpa"))
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<PassengerFactory>().angry;
                        coll.gameObject.GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<PassengerFactory>().angry;
                        isAngry = true;
                        coll.gameObject.GetComponent<MoveController>().isAngry = true;

                    }
                }
            }
        }
        if (coll.gameObject.CompareTag("Seat"))
        {
            xToSnap = coll.gameObject.GetComponentInParent<Transform>().transform.position.x;
            _selectedColumn++;
            //_seatTaken = coll.gameObject.GetComponent<SeatLogic>().passanger_Seating;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Passenger"))
        {
            if (coll.GetComponent<MoveController>()._selectedRow < 2)
            {
                _blockMoveUp = false;
                Debug.Log("Free move up");
            }
            else if (coll.GetComponent<MoveController>()._selectedRow > 2)
            {
                _blockMoveDown = false;
                Debug.Log("Free move down");
            }
            if (coll.gameObject.CompareTag("Column"))
            {
                _blockMoveUp = false;
                _blockMoveDown = false;
            }
        }
        if (!this.gameObject.GetComponent<MoveController>().isSeated)
        {
            if ((this.gameObject.GetComponent<MoveController>()._selectedRow == 2)&&
                ((coll.gameObject.GetComponent<MoveController>()._selectedRow != 3) || (coll.gameObject.GetComponent<MoveController>()._selectedRow != 1)) )
            {
                Debug.Log(coll.gameObject.GetComponent<SpriteRenderer>().sprite.name);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<PassengerFactory>().newSprite;
                isAngry = false;
            }
            if ((this.gameObject.GetComponent<MoveController>()._selectedRow == 2) &&
                ((coll.gameObject.GetComponent<MoveController>()._selectedRow == 3) || (coll.gameObject.GetComponent<MoveController>()._selectedRow == 1)))
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<PassengerFactory>().newSprite;
                isAngry = false;
            }
            if (((coll.gameObject.GetComponent<MoveController>()._selectedRow == 4) || (coll.gameObject.GetComponent<MoveController>()._selectedRow == 0)))
            {
                coll.gameObject.GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<PassengerFactory>().newSprite;
                coll.gameObject.GetComponent<MoveController>().isAngry = false;
            }
        }
    }


    void Update()
    {
        //Debug.Log(this.gameObject.name+""+_blockMoveUp);
        switch (state)
        {
            //this is the first stage in which the passengers only moving right *constantly*
            case State.moving:

                //sprite allways moves to the right
                
                transform.position = Vector3.MoveTowards(transform.position,MovePoint.position, speed * Time.deltaTime );

                //turn "hora" for control indicator on \ off
                if (isControlled)
                {
                    Hora.gameObject.SetActive(true);
                    HoraRegular.gameObject.SetActive(false);
                }
                else if (!isControlled)
                {
                    Hora.gameObject.SetActive(false);
                    HoraRegular.gameObject.SetActive(true);
                }

                //make the controlled player sprint
                if (Input.GetKeyDown(KeyCode.D) && isControlled == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, speed * sprint * Time.deltaTime);
                }

                //If the player leaves the screen
                //if (transform.position.x > 7.4f)
                //{
                //This was moved to the collider of Death - Ori
                //}

                if (isControlled == true /*&& (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))*/)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = State.seating;
                        TakeASeat("w");
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = State.seating;
                        TakeASeat("s");

                    }
                }

                break;

            //this is the state in which we control the player.
            case State.seating:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    TakeASeat("w");                    
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    TakeASeat("s");
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (_selectedRow == 2)
                    {
                        transform.Translate(Vector2.right * speed * sprint * Time.deltaTime);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_selectedRow != 2)
                    {
                        Hora.gameObject.SetActive(false);
                        HoraRegular.gameObject.SetActive(false);
                        //_seatTaken.gameObject.GetComponent<SeatLogic>().seated = true;
                        //_seatTaken.gameObject.GetComponent<SeatLogic>().passanger_Seating = this.gameObject;
                        isControlled = false;
                        isSeated = true;
                        m_ObjectCollider.GetComponent<BoxCollider2D>().isTrigger = true;
                        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        state = State.seated;
                    }
                }

                break;
            
                //this is the final state.
            case State.seated:
 
                if (this.lastOne)
                {
                    if(this.isSeated)
                    {
                        GameObject controller = GameObject.Find("Controller");
                        controller.GetComponent<gameController>().HasFinished = true;
                    }
                }
                break;

        }
      
    }

    private void TakeASeat(string keyDown)
    {
        initialPos = this.gameObject.GetComponent<Transform>();
        
        if (keyDown == "w")
        {
            if (_selectedRow == 0)
            {
                return;
                //Top row
            }            
            else if (this._blockMoveUp == false)
            {
                transform.position = new Vector2(xToSnap, Rows[_selectedRow - 1].position.y);
                _selectedRow--;
            }
        }
        else if (keyDown == "s")
        {
            if (_selectedRow == 4)
            {
                return;
                //Bottom row
            }
            else if(!this._blockMoveDown)
            {
                transform.position = new Vector2(xToSnap, Rows[_selectedRow + 1].position.y);
                _selectedRow++;
            }
        }
    }
    private void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
