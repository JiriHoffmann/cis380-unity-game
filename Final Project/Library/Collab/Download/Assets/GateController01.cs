using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController01 : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("WM2");
        }
        /*if (collision.gameObject.name == "Player")
        {
            animator.SetBool("Opening", true);
        }
        */
    }
}