using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 10f;
    public float seatStep = 10f;

    private bool _isSeated;
    private enum State { moving, seating, seated };
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.moving;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.moving:
                if (Input.GetKeyDown(KeyCode.W) )
                {
                    state = State.seating;
                    TakeASeat();
                }

                else
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                }

                break;

            case State.seating:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    TakeASeat();
                }

                break;

            case State.seated:

                break;

        }
        //if (Input.GetKey(KeyCode.W) && !_isSeated)
        //{
        //    _isSeated = true;
        //}
        //if (_isSeated == false)
        //{
        //    transform.Translate(Vector2.right * Time.deltaTime * speed);
        //}
        //transform.position = Vector3.forward * Time.deltaTime * 0.5f;
    }

    private void TakeASeat()
    {
        //transform.position = Vector2.up *5f;
        transform.Translate(Vector2.up * Time.deltaTime * speed * seatStep);

    }
}
