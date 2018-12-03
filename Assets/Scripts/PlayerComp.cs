using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComp : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _moveRaw;

    private Transform _transform;
    public Transform Transform 
    {
        get
        {
            return _transform;
        }
    }

    private bool _allowMovement;
    public bool IsAllowingMovement 
    {
        get 
        {
            return _allowMovement;
        }
    }

    public void Initialize()
    {
        _transform = GetComponent<Transform>();
        _allowMovement = true;
    }

    public void SetAllowMovements(bool allowMovement)
    {
        _allowMovement = allowMovement;
    }
	
	void FixedUpdate ()
    {
        if (!_allowMovement) return;

        float currX = _transform.position.x;
        float currY = _transform.position.y;

        float horizontal = (_moveRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal")) * _speed;
        float vertical = (_moveRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical")) * _speed;

        float newX = currX + (horizontal * Time.deltaTime);
        float newY = currY + (vertical * Time.deltaTime);

        _transform.position = new Vector3(newX, newY, _transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.CheckPressButtonAction(transform.position);
        }
	}
}
