using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAnimEventRelay : MonoBehaviour
{

    [SerializeField] GameObject CharacterController;

    // Start is called before the first frame update
    public void AnimationStart()
    {
        CharacterController.GetComponent<CharacterAttackController>().CalculateDamage();
    }
}
