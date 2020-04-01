using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Passenger's Attributes")]

    public float speed = 5f;
    public float seatStep = 10f;

    [Header("Passenger's State")]
    // These should stay public -> being used from another script (PassArray)
    public bool isControlled = false;
    public bool isSeated = false;

    private enum State { moving, seating, seated };
    private State state;


    void Start()
    {
        state = State.moving;
    }

    void Update()
    {
        switch(state)
        {
            //this is the first stage in which the passengers only moving right *constantly*
            case State.moving:
                
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.Translate(Vector2.right * speed * 10 * Time.deltaTime);
                }
                
                if(isControlled == true && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))
                {
                    state = State.seating;
                }

                break;

            //this is the state in which we control the player.
            case State.seating:

               TakeASeat();              
                
                if(isSeated == true)
                {
                    state = State.seated;
                }
                break;
            
                //this is the final state.
            case State.seated:

                break;

        }
      
    }

    private void TakeASeat()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed * seatStep);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed * seatStep);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSeated = true;
        }
    }
}
