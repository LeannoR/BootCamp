using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Transform noozle;
    public GameObject rockPrefab;
    public float rockSpeed = 10f;
    public float fireRate = 1f;
    public float nextFire = 0f;

    public void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var rock = Instantiate(rockPrefab, noozle.position, noozle.rotation);
            rock.GetComponent<Rigidbody>().velocity = noozle.forward * rockSpeed;
        }
    }
}
