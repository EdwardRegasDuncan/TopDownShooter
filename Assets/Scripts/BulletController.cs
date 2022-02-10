using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    float speed = 20f;
    int damage = 100;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Collider[] enemy = Physics.OverlapSphere(transform.position, transform.lossyScale.x, LayerMask.GetMask("Enemy"));
        if (enemy.Length > 0)
        {
            enemy[0].GetComponent<EnemyController>().AdjustHealth(-damage);
            expireBullet();
        }
    }

    private void Awake()
    {
        StartCoroutine("DeathTimer");
    }

    public IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);
        expireBullet();
    }

    void expireBullet()
    {
        Destroy(this.gameObject);
    }
}
