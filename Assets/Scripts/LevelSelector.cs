using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("SelectLevel");
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene("L" + level);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
