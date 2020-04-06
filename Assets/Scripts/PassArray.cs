using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassArray : MonoBehaviour
{
    [Header("List's / Level Settings")]

    public GameObject passengersPrefab;
    public Transform startingPoint;
    public int numberOfPassengers;
    public List<GameObject> passList;

    public float spawnDelayStart = 7f;
    private float spawnDelay;

    private GameObject currPass;

    private bool hasStarted = false;
    private bool hasFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchPassengers());
        spawnDelay = spawnDelayStart;
}


    IEnumerator LaunchPassengers()
    {
        yield return new WaitForSeconds(2);
        
        //makes sure they match length
        for (int i = 0; i < numberOfPassengers; i++)
        {
            spawnDelay = spawnDelayStart;
            passList.Add(Instantiate(passengersPrefab, new Vector3(startingPoint.position.x, startingPoint.position.y, startingPoint.position.z), Quaternion.identity) as GameObject);
            hasStarted = true;
            yield return new WaitForSeconds(spawnDelay);
        }
        hasFinished = true;
    }


    void Update()
    {
        if(hasStarted == true && passList.Count > 0)
        {
            ChangeControl();
        }
        if (hasFinished)
        {
            currPass.GetComponent<MoveController>().lastOne = true;
        }

    }

    void ChangeControl()
    {
        currPass = passList[0];
        currPass.GetComponent<MoveController>().isControlled = true;

        if (currPass.GetComponent<MoveController>().isSeated == true)
        {
            passList.RemoveAt(0);
            currPass = passList[0];
            spawnDelay = 0;

        }
    }
}
