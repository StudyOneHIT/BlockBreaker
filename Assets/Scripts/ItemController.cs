using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
	private GameController gc;

	public float timeForItem;
	public float slowScale; // For Item 'SlowDown'
	public GameObject[] itemPrefabs;
	public float ItemRate;
	public int MaxItem;
	public GameObject StickySprite;
	public GameObject OppositeSprite;

	// Items
	public const int itemNum = 6;
	public const int SLOWDOWN = 0;
	public const int STICKY = 1;
	public const int OPPOSITE = 2;
	public const int SPLIT = 3;
	public const int LARGER = 4;
	public const int SHORTER = 5;

	private BoardMove bm;
	private float[] timeLeft;
	private bool[] ItemEnabled = new bool[6];
	private int itemCount = 0;
	private float dtimer = 0;

	public static int GetItemIndex(string item) {
		if (item == "SlowDown") {
			return SLOWDOWN;
		} else if (item == "Split") {
			return SPLIT;
		} else if (item == "LargerBoard") {
			return LARGER;
		} else if (item == "ShorterBoard") {
			return SHORTER;
		} else if (item == "Opposite") {
			return OPPOSITE;
		} else if (item == "Sticky") {
			return STICKY;
		}
		return -1;
	}

	public void NewItem(Transform transform) {
		System.Random random = new System.Random();
		if (random.Next(0, 100) <= ItemRate * 100 && itemCount <= MaxItem) {
		//if (true) {
			int iItem = random.Next(0, itemNum);
			//int iItem = 1;
			if (ItemEnabled[iItem]) return;
			Instantiate(itemPrefabs[iItem], transform.position, transform.rotation);
			ItemEnabled[iItem] = true;
		}
	}

	public void UseItem(string item) {
		itemCount++;
		if (item == "SlowDown") {
			foreach (GameObject b in gc.balls) {
				b.GetComponent<BallActions>().MySetVelocityScale(slowScale);
			}
			timeLeft[SLOWDOWN] += timeForItem;
			ItemEnabled[SLOWDOWN] = true;
		} else if (item == "Split") {
			if (gc.ballNum >= 1) {
				gc.NewBall(gc.balls[0], 0.3F);
				gc.NewBall(gc.balls[0], -0.3F);
				ItemEnabled[SPLIT] = true;
			}
		} else if (item == "LargerBoard") {
			bm.MyScale(1);
			ItemEnabled[LARGER] = true;
		} else if (item == "ShorterBoard") {
			bm.MyScale(-0.6F);
			ItemEnabled[SHORTER] = true;
		} else if (item == "Opposite") {
			bm.opposite = true;
			OppositeSprite.SetActive(true);
			timeLeft[OPPOSITE] += timeForItem;
			ItemEnabled[OPPOSITE] = true;
		} else if (item == "Sticky") {
			bm.sticky = true;
			StickySprite.SetActive(true);
			timeLeft[STICKY] += timeForItem;
			ItemEnabled[STICKY] = true;
		} else {
			itemCount--;
		}
	}

	public void RecycleItem(string item) {
		ItemEnabled[GetItemIndex(item)] = false;
	}

	void DetectItem() {
		for (int i = 0; i < 3; i++) {
			if (timeLeft[i] > 0) {
				timeLeft[i]--;
				if (timeLeft[i] <= 0) {
					itemCount--;
					if (i == SLOWDOWN) {
						foreach (GameObject b in gc.balls) {
							b.GetComponent<BallActions>().MySetVelocityScale(1.0F / slowScale);
						}
						ItemEnabled[SLOWDOWN] = false;
					} else if (i == OPPOSITE) {
						bm.opposite = false;
						OppositeSprite.SetActive(false);
						ItemEnabled[OPPOSITE] = false;
					} else if (i == STICKY) {
						bm.sticky = false;
						StickySprite.SetActive(false);
						ItemEnabled[STICKY] = false;
					} else {
						itemCount++;
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		timeLeft = new float[itemNum];
		for (int i = 0; i < itemNum; i++) {
			timeLeft[i] = -1;
		}
		bm = GameController.instance.board.GetComponent<BoardMove>();
		gc = GameController.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (dtimer > 0) {
			dtimer -= Time.deltaTime;
		} else if (dtimer < 0) {
			dtimer = 0;
		} else {
			dtimer = 1;
			DetectItem();
		}
		if (gc.ballNum <= 1) {
			ItemEnabled[SPLIT] = false;
		}
	}
}
