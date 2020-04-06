using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameController : MonoBehaviour
{

    public Transform Row1;
    public Transform Row2;
    public Transform Row3;
    public Transform Row4;
    public Transform Aisle;

    [SerializeField] float timeLeft;
    [SerializeField] Text timerText;
    [SerializeField] GameObject endGameScreen;
    public GameObject winScreen;

    [SerializeField] float spawnTime;
    [SerializeField] Text SpwnText;

    [SerializeField] Text CountPass;


    public bool HasStarted = false;
    public bool HasFinished = false;

    bool clock;

    void Start()
    {
    }

    void Update()
    {
        if (!HasFinished)
        {
            if (timeLeft > 0 && clock == false)
            {
                clock = true;
                StartCoroutine(WaitforTimer());
                if (HasStarted)
                {
                    StartCoroutine(WaitforSpawn());
                }
            }
            else if (timeLeft == 0)
            {
                Time.timeScale = 0f;
                endGameScreen.SetActive(true);
            }

        }
        else if (HasFinished)
        {
            winScreen.gameObject.SetActive(true);
        }

    }
    IEnumerator WaitforTimer()
    {

        timeLeft -= 1;
        UpdateTimer();
        yield return new WaitForSeconds(1);
        clock = false;
    }

    IEnumerator WaitforSpawn()
    {
        spawnTime -= 1;
        if (spawnTime > 0 )
        {
            int spawnSec = Mathf.FloorToInt(spawnTime % 60);
            SpwnText.GetComponent<UnityEngine.UI.Text>().text = /*min.ToString("0") + ":" +*/ spawnSec.ToString("00");
        }
        else if (spawnTime == 0)
        {
            int spawnSec = Mathf.FloorToInt(spawnTime % 60);
            spawnTime = 4;
            SpwnText.GetComponent<UnityEngine.UI.Text>().text = /*min.ToString("0") + ":" +*/ spawnSec.ToString("00");
        }
        yield return new WaitForSeconds(1);
    }


    void UpdateTimer()
    {

        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        timerText.GetComponent<UnityEngine.UI.Text>().text = min.ToString("0") + ":" + sec.ToString("00");

    }


}
