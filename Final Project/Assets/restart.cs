using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(startingGame());
    }

    IEnumerator startingGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StartupScreen");
    }
}