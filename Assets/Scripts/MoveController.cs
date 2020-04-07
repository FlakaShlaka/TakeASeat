using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Passenger's Attributes")]

    public float speed = 5f;
    public float sprint = 15f;

    [Header("Passenger's State")]
    // These should stay public -> being used from another script (PassArray)
    public bool isControlled = false;
    public GameObject Hora;
    public bool isSeated = false;

    private enum State { moving, seating, seated };
    private State state;

    [Header("Seats Attributes")]
    private List<Transform> Rows = new List<Transform>(5);
    private int _selectedRow = 2;

    public bool lastOne = false;

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

    void Update()
    {

        switch (state)
        {
            //this is the first stage in which the passengers only moving right *constantly*
            case State.moving:
                transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (isControlled)
                {
                    Hora.gameObject.SetActive(true);
                }
                else if (!isControlled)
                {
                    Hora.gameObject.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.Translate(Vector2.right * speed * sprint * Time.deltaTime);
                }

                //If the player leaves the screen
                if (transform.position.x > 7.4f)
                {
                    isSeated = true;
                    state = State.seated;                    
                }

                if (isControlled == true /*&& (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))*/)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        TakeASeat("w");
                        state = State.seating;
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        TakeASeat("s");
                        state = State.seating;
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Hora.gameObject.SetActive(false);
                    isSeated = true;                    
                    state = State.seated;
                }

                break;
            
                //this is the final state.
            case State.seated:

                if (lastOne)
                {
                    GameObject controller = GameObject.Find("Controller");
                    controller.GetComponent<gameController>().HasFinished = true;
                }


                break;

        }
      
    }

    private void TakeASeat(string keyDown)
    {

        if (keyDown == "w")
        {
            if (_selectedRow == 0)
            {
                return;
                //Top row
            }
            else
            {
                transform.position = new Vector2(transform.position.x, Rows[_selectedRow - 1].position.y);
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
            else
            {
                transform.position = new Vector2(transform.position.x, Rows[_selectedRow + 1].position.y);
                _selectedRow++;
            }
        }
    }
}
