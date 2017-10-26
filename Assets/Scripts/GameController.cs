using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject board;
	public GameObject ballPrefab;
	public GameObject pauseMenu;
    public GameObject failMenu;

    public bool started = false;
	public int ballNum = 1;
	public int life = 3;
    public bool mute = false;

	public void StartGame()
	{
		SceneManager.LoadScene("SelectLevel");
	}

	public void DecLife()
	{
		started = false;
		life--;
		ballNum = 1;
		if (life < 1) {
            // Game Over
            failMenu.gameObject.SetActive(true);
		} else {
			Instantiate (ballPrefab);
		}
	}

	public void DecBall()
	{
		ballNum--;
		if (ballNum < 1) 
		{
			DecLife ();
		}
	}

	public void IncBall()
	{
		ballNum++;
	}

	public void Pause()
	{
		// Show the menu.
		pauseMenu.gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	public void Resume()
	{
		// Hide the menu.
		pauseMenu.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void SoundSwitch()
    {
        mute = !mute; 
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
