using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] int health = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int value)
    {
        health = value;
    }
    public void AdjustHealth(int value)
    {
        health += value;
    }

}
