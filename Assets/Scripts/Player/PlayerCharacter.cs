using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject emitter = null;
    public GameObject bulletPrefab = null;

    private void Start()
    {
        GameplayModule.OnType += HandleOnType;
    }

    private void OnDestroy()
    {
        GameplayModule.OnType -= HandleOnType;
    }

    private void HandleOnType(bool isCorrect, Transform target)
    {
        if (isCorrect && target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, rotation.z, transform.rotation.w);

            GameObject bullet = Instantiate(bulletPrefab, emitter.transform.position, emitter.transform.rotation);
            bullet.transform.rotation = new Quaternion(bullet.transform.rotation.x, bullet.transform.rotation.y, rotation.z, bullet.transform.rotation.w);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.target = target.gameObject;
        }
        else
        {

        }
    }
}
