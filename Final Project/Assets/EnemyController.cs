using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    Rigidbody2D rigBod;
    public bool isFacingRight = false;
    public int maxHealth = 100;
    public int currentHealth;
    // public Animator animator;
    Animator animator;
    public BossBar BossBar = null;
    int numhbs = 3;
    // Start is called before the first frame update
    void Start()
    {
        if(BossBar){
            BossBar.setMaxHealth(maxHealth);
            BossBar.setHealth(maxHealth);
        }

        currentHealth = maxHealth;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (isFacingRight)
        {
            enemy.transform.Rotate(0f, 180f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rigBod.velocity.x));
        if (rigBod.velocity.x > 0 && !isFacingRight)
        {
            enemy.transform.Rotate(0f, -180f, 0f);
            isFacingRight = true;
        }
        else if (rigBod.velocity.x < 0 && isFacingRight)
        {
            enemy.transform.Rotate(0f, 180f, 0f);
            isFacingRight = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (BossBar){
            BossBar.setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                if (numhbs == 0){
                    Instantiate(Resources.Load("EnemyExplode"), transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else{
                    numhbs--;
                    currentHealth = maxHealth;
                }
            }
        }

        else if (!BossBar && currentHealth <= 0)
        { 
            Instantiate(Resources.Load("EnemyExplode"), transform.position, transform.rotation);
            Destroy(gameObject);    
        }

    }
}
