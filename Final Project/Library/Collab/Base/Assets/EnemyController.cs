using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    Rigidbody2D rigBod;
    public bool isFacingRight = false;
    public int maxHealth = 100;
    public int currentHealth;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        enemy.transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rigBod.velocity.x));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Instantiate(Resources.Load("EnemyExplode"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
