using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateControllers : MonoBehaviour
{
    Animator animator;
    public GameObject thisObject;
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
            SceneManager.LoadScene(thisObject.name);
        }
        /*if (collision.gameObject.name == "Player")
        {
            animator.SetBool("Opening", true);
        }
        */
    }
}