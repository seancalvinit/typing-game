using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0f;

    public GameObject target = null;

    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, rotation.z, transform.rotation.w);
        }

        float deltaSpeed = Time.deltaTime * speed;
        transform.position += transform.right * deltaSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target == collision.gameObject)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.Hurt();
            Destroy(gameObject);
        }
    }
}
