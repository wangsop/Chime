using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : AItem
{
    // Bullet object
    public GameObject bullet;
    public float fireRate = 1.2f;
    private float _lastFireTime;

    public float AttackRadius = 15f;

    private GameObject _Closest;
    public GameObject Closest { get { return _Closest; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastFireTime >= fireRate)
        {
            _Closest = GetClosestEnemy();
            if (_Closest != null)
            {
                if (Vector3.Distance(transform.position, _Closest.transform.position) < AttackRadius)
                {
                    Rigidbody r = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    r.velocity = (_Closest.transform.position - transform.position).normalized * 10;
                    _lastFireTime = Time.time;
                }
            }
        }
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 1000;
        GameObject closestObject = null;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float dist = Vector3.Distance(gameObjects[i].transform.position, transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestObject = gameObjects[i];
            }
        }
        return closestObject;
    }

    public override void PrimaryAction()
    {
        GameObject player = GameObject.FindWithTag("Player");
        var dropPos = player.transform.position + player.transform.forward * 2.0f;
        currentInv.DropItem();
    }
}
