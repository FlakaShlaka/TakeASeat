using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class gameController : MonoBehaviour
{

    public Transform Row1;
    public Transform Row2;
    public Transform Row3;
    public Transform Row4;
    public Transform Aisle;

    public int lastIndex;

    [SerializeField] float timeLeft;
    [SerializeField] Text timerText;
    [SerializeField] GameObject endGameScreen;

    [SerializeField] float spawnTime;
    [SerializeField] Text SpwnText;

    [SerializeField] Text CountPass;


    public bool HasStarted = false;
    public bool HasFinished = false;

    bool clock;

    void Start()
    {
        lastIndex = UnityEngine.Random.Range(0, 10);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                MoveController[] components = GameObject.FindObjectsOfType<MoveController>();
                int _score = 0;
                foreach (var item in components)
                {
                    if (!item.isAngry && item.isSeated)
                    {
                        _score++;
                    }
                }
                if (_score <= 13)
                {
                    GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                    chidlran[0].gameObject.SetActive(true);
                }
                if (_score > 13 && _score <= 26)
                {
                    GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                    chidlran[1].gameObject.SetActive(true);
                }
                if (_score > 26 && _score < 40)
                {
                    GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                    chidlran[1].gameObject.SetActive(true);
                }
                if (_score == 40)
                {
                    GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                    chidlran[2].gameObject.SetActive(true);
                }
            }

        }
        else if (HasFinished)
        {
            endGameScreen.gameObject.SetActive(true);
            MoveController[] components = GameObject.FindObjectsOfType<MoveController>();
            int _score = 0;
            foreach (var item in components)
            {
                if (!item.isAngry && item.isSeated)
                {
                    _score++;
                }
            }
            if (_score <= 13)
            {
                GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                chidlran[0].gameObject.SetActive(true);
            }
            if (_score > 13 && _score <= 26)
            {
                GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                chidlran[1].gameObject.SetActive(true);
            }
            if (_score > 26 && _score < 40)
            {
                GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                chidlran[1].gameObject.SetActive(true);
            }
            if (_score == 40)
            {
                GameObject[] chidlran = endGameScreen.gameObject.GetComponent<ScreensContainer>()._screens;
                chidlran[2].gameObject.SetActive(true);
            }
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
