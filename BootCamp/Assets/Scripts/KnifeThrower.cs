using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    public Transform noozle;
    public GameObject knifePrefab;

    public float knifeSpeed = 10f;
    public float fireRate = 2f;
    public float nextFire = 1f;

    public void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var knife = Instantiate(knifePrefab, noozle.position, noozle.rotation);
            knife.GetComponent<Rigidbody>().velocity = noozle.forward * knifeSpeed;
        }
    }
}
