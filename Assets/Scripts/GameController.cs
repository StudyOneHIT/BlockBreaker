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
	public GameObject successMenu;
	public GameObject bottom;
	public GameObject itemController;
	public GameObject bgmObject;

	public GameObject StarSprite;
	public GameObject TwoStarsSprite;
	public GameObject ThreeStarsSprite;

	public int MaxCombo;
	public float MaxTime;
	private float startTime;
	[HideInInspector] public int Star2 = 0;
	[HideInInspector] public int Star3 = 0;

	public float ShootSpeed;
	public float MinTan;
	public int BrickNum;

	[HideInInspector] public List<GameObject> balls;
	[HideInInspector] public int ballNum = 1;
	[HideInInspector] public int life = 3;
	[HideInInspector] public float maxWidth;
	[HideInInspector] public ItemController ic;
	[HideInInspector] public bool mobile = false;
	
	//private BoardMove bm;
	private bool success = false;
	
	public void DecLife() {
		if (!success) {
			life--;
			ballNum = 1;
			if (life < 1) {
				// Game Over
				failMenu.gameObject.SetActive(true);
			} else {
				balls.Add(Instantiate(ballPrefab));
			}
		}
	}

	public void DecBall(GameObject ball) {
		ballNum--;
		balls.Remove(ball);
		Destroy(ball);
		if (ballNum < 1) {
			DecLife();
		}
	}

	public void DecBrick(GameObject brick) {
		Destroy(brick);
		BrickNum--;
		if (BrickNum < 1) {
			Success();
		}
	}

	public void Success() {
		if (Time.time - startTime < MaxTime) {
			Star3 = 1;
		}
		success = true;
		successMenu.SetActive(true);
		switch (Star2 + Star3) {
			case 0:
				StarSprite.SetActive(true);
				break;
			case 1:
				TwoStarsSprite.SetActive(true);
				break;
			case 2:
				ThreeStarsSprite.SetActive(true);
				break;
			default:
				break;
		}
	}

	/*
	public GameObject IncBall() {
		ballNum++;
		GameObject go = Instantiate(ballPrefab); // TODO: the position
		balls.Add(go);
		return go;
	}
	*/

	public void Pause() {
		// Show the menu.
		pauseMenu.gameObject.SetActive(true);
		Time.timeScale = 0;
	}

	public void Resume() {
		// Hide the menu.
		pauseMenu.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	public void Restart() {
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

	public void NextLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void NewBall(GameObject old, float offset) {
		Vector2 vv = old.GetComponent<Rigidbody2D>().velocity;
		double theta = System.Math.Atan2(vv.y, vv.x);
		float vabs = (float)System.Math.Sqrt(vv.x * vv.x + vv.y * vv.y);
		Transform t = old.transform;
		GameObject game = Instantiate(ballPrefab, t.position, t.rotation);
		game.GetComponent<BallActions>().sticked = old.GetComponent<BallActions>().sticked;
		game.GetComponent<Rigidbody2D>().velocity = new Vector2(
			vabs * (float)System.Math.Cos(theta + offset), vabs * (float)System.Math.Sin(theta + offset));
		balls.Add(game);
		ballNum++;
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		maxWidth = bottom.GetComponent<Collider2D>().bounds.size.x;
	}

	// Use this for initialization
	void Start() {
		balls = new List<GameObject>();
		balls.Add(Instantiate(ballPrefab));
		ic = itemController.GetComponent<ItemController>();
		//bm = board.GetComponent<BoardMove>();
		mobile = (Application.platform == RuntimePlatform.Android);
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey("space") || 
			(mobile && Input.touches[0].phase == TouchPhase.Moved)) {
			foreach (GameObject ball in balls) {
				BallActions ba = ball.GetComponent<BallActions>();
				if (ba.sticked) {
					ba.MyShoot();
					break;
				}
			}
		} else if (mobile && (Input.GetKeyDown(KeyCode.Escape))) {
			Pause();
		}
	}

}
