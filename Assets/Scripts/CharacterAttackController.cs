using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{

    bool meleeCooldown = false;
    bool rangeCooldown = false;
    [SerializeField] Transform meleeRange;
    int meleeDamage = 100;

    [SerializeField] Animator meleeAnim;
    CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //left mouse button
        {
            if (!meleeCooldown)
            {
                cc.attacking = true;
                MeleeAttack();
                meleeCooldown = true;
                StartCoroutine("MeleeCooldown");
            }
        }
        if (Input.GetMouseButtonDown(1)) //right mouse button
        {
            if (!rangeCooldown)
            {
                rangeCooldown = true;
                StartCoroutine("RangeCooldown");
                if (meleeCooldown)
                {
                    SpreadShot();
                }
                else
                {
                    RangedAttack();
                }
                
            }
            
        }

    }

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunBarrel;
    void RangedAttack()
    {
        Instantiate(bullet, gunBarrel.position, gunBarrel.rotation);
    }
    [SerializeField] Transform[] spreadshotPositions;
    void SpreadShot()
    {
        foreach (Transform position in spreadshotPositions)
        {
            Instantiate(bullet, position.position, position.rotation);
        }
    }


    void MeleeAttack()
    {
        meleeAnim.SetTrigger("Attack");
    }
    public void CalculateDamage()
    {
        Collider[] enemiesInRange = Physics.OverlapBox(meleeRange.position, meleeRange.lossyScale / 2, meleeRange.rotation, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in enemiesInRange)
        {
            enemy.GetComponent<EnemyController>().AdjustHealth(-meleeDamage);
        }
    }
    public IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(1f);
        meleeAnim.ResetTrigger("Attack");
        meleeCooldown = false;
        cc.attacking = false;
    }
    public IEnumerator RangeCooldown()
    {
        yield return new WaitForSeconds(1f);
        rangeCooldown = false;
    }

}
