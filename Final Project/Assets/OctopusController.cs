
using UnityEngine;

public class OctopusController : MonoBehaviour
{
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0f, 1.5f);
    }

    // Update is called once per frame

    void LaunchProjectile()
    {
        GameObject bullet = Instantiate(Resources.Load("EnemyBullet"), firePoint.position, firePoint.rotation) as GameObject;
    }

    void Update()
    {

    }
}
