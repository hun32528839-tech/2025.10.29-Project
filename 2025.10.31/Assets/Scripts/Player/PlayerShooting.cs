using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform muzzleTransform;  
    public GameObject bulletPrefab;     
    public float bulletSpeed = 30f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = muzzleTransform.forward * bulletSpeed;
        }
    }
}
