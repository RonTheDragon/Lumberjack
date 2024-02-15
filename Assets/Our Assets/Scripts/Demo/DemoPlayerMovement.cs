using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerMovement : MonoBehaviour
{
    private CharacterController _characterController => GetComponent<CharacterController>();
    [SerializeField] private float _speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            _characterController.Move(transform.forward * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _characterController.Move(-transform.forward * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _characterController.Move(transform.right * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _characterController.Move(-transform.right * _speed * Time.deltaTime);
        }
    }
}
