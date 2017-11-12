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
    public float timeForItem;
    public float slowScale;

    // Items
    public const int SLOWDOWN = 0;
    public const int STICKY = 1;
    public const int OPPOSITE = 2;

    private List<GameObject> balls;
    public List<GameObject> Balls
    {
        get { return balls; }
    }

    private int ballNum = 1;
    public int BallNum
    {
        get { return ballNum; }
    }

    private int life = 3;
    public int Life
    {
        get { return life; }
    }

    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
    }

    private int itemNum = 3;
    private float[] timeLeft;


    

    public void DecLife() {
        life--;
        ballNum = 1;
        if (life < 1) {
            // Game Over
            failMenu.gameObject.SetActive(true);
        }
        else {
            balls.Add(Instantiate(ballPrefab));
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

    public GameObject IncBall() {
        ballNum++;
        GameObject go = Instantiate(ballPrefab); // TODO: the position
        balls.Add(go);
        return go;
    }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void SoundSwitch() {
        mute = !mute;
    }

    public void UseItem(string item) {
        if (item == "SlowDown") {
            foreach (GameObject b in balls) {
                b.GetComponent<BallActions>().MySetVelocityScale(slowScale);
            }
            timeLeft[0] += timeForItem;
        }
        else if (item == "Split") {
            if (ballNum == 1) {
                NewBall(balls[0], 0.3F);
                NewBall(balls[0], -0.3F);
            }
            else if (ballNum == 2) {

            }
        }
    }

    public void DisableItem(int item) {
        if (item == SLOWDOWN) {
            foreach (GameObject b in balls) {
                b.GetComponent<BallActions>().MySetVelocityScale(1.0F / slowScale);
            }
        }
    }

    public void NewBall(GameObject old, float offset) {
        Vector2 vv = old.GetComponent<Rigidbody2D>().velocity;
        double theta = System.Math.Atan2(vv.y, vv.x);
        float vabs = (float)System.Math.Sqrt(vv.x * vv.x + vv.y * vv.y);
        Transform t = old.transform;
        GameObject game = Instantiate(ballPrefab, t.position, t.rotation);
        game.GetComponent<BallActions>().Sticked = old.GetComponent<BallActions>().Sticked;
        game.GetComponent<Rigidbody2D>().velocity = new Vector2(
            vabs * (float)System.Math.Cos(theta + offset), vabs * (float)System.Math.Sin(theta + offset));
        balls.Add(game);
        ballNum++;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start() {

        balls = new List<GameObject>();
        balls.Add(Instantiate(ballPrefab));
        timeLeft = new float[itemNum];
        for (int i = 0; i < itemNum; i++) {
            timeLeft[i] = -1;
        }
    }

    private float dtimer = 0;

    void DetectItem() {
        for (int i = 0; i < itemNum; i++) {
            if (timeLeft[i] > 0) {
                timeLeft[i]--;
                if (timeLeft[i] <= 0) {
                    DisableItem(i);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (dtimer > 0) {
            dtimer -= Time.deltaTime;
        }
        else if (dtimer < 0) {
            dtimer = 0;
        }
        else {
            dtimer = 1;
            DetectItem();
        }
        if (Input.GetKey("space")) // TODO: change to touch-screen operation.
        {
            foreach (GameObject ball in balls) {
                BallActions ba = ball.GetComponent<BallActions>();
                if (ba.Sticked) {
                    ba.MyShoot();
                    break;
                }
            }
        }


    }

}
