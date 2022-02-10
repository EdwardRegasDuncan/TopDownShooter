using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject _characterModel;
    [SerializeField] GameObject _weaponModel;
    [SerializeField] LayerMask ground;

    public bool attacking = false;

    Vector3 direction;


    Vector3 _aimPoint;

    float _moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (direction.magnitude > 0.1f)
        {
            transform.Translate(direction * _moveSpeed * Time.deltaTime, Space.World);
        }
    }
    // Update is called once per frame
    void Update()
    {

        //movement
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        //rotate character towards mouse
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out var hitinfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            _aimPoint = hitinfo.point;
            Vector3 angle = _characterModel.transform.position - hitinfo.point;
            Vector3 weaponAngle = _weaponModel.transform.position - hitinfo.point;
            angle.y = 0;
            weaponAngle.y = 0;
            _characterModel.transform.forward = -angle;
            if (!attacking)
            {
                _weaponModel.transform.forward = -weaponAngle;
            }
            
        }


        // Rotate Character based on screen space instead of world space
        /*Vector2 characterPositionInScreenSpace = Camera.main.WorldToViewportPoint(_characterModel.transform.position);
        Vector2 mousePositionInScreenSpace = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 angle = characterPositionInScreenSpace - mousePositionInScreenSpace;
        _characterModel.transform.forward = new Vector3(-angle.x, 0, -angle.y);*/

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_aimPoint, 0.5f);
        Gizmos.DrawLine(_characterModel.transform.position, _aimPoint);
    }
}
