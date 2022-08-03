using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    [SerializeField]
    public float bulletSpeed;
    public float bulletDamage;

    Vector3 targetPosition;



    public void Start()
    {
        Shoot();
    }

    public void Shoot()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        print("Bullet instantiated!");
        print("Looking for target");
        targetPosition = Camera.main.transform.position;

        print("Target Found");

        while (this.transform.position != targetPosition)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * bulletSpeed);
            yield return new WaitForEndOfFrame();
        }

        StopCoroutine(Move());
        Destroy(this.gameObject);

        yield return null;
    }
}
