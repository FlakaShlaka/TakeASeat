using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassArray : MonoBehaviour
{
    public GameObject passengers;
    public Transform startingPoint;
    public int numberOfPassengers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchPassengers());
    }


    IEnumerator LaunchPassengers()
    {
        yield return new WaitForSeconds(2);

        //makes sure they match length
        for (int i = 0; i < numberOfPassengers; i++)
        {
            yield return new WaitForSeconds(2);
            passengers = Instantiate(passengers, new Vector3(startingPoint.position.x, startingPoint.position.y, startingPoint.position.z), Quaternion.identity) as GameObject;
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
