using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Startup : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(startingGame());
        PlayerPrefs.SetInt("health", 100);
        PlayerPrefs.SetInt("hbs", 3);
    }

    IEnumerator startingGame()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("WM1");
    }
}