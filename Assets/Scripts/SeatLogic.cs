using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatLogic : MonoBehaviour
{
    public bool seated;
    public GameObject passanger_Seating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (passanger_Seating !=null)
        {
            Debug.Log(passanger_Seating);

        }
    }


}
