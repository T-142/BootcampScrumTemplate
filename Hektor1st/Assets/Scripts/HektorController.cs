using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HektorController : MonoBehaviour
{
    Vector3 _input;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = 360.0f;
    [SerializeField] GameObject _bomb;
    [SerializeField] Transform _bombHole;
    [SerializeField] private float _velocity = 10;
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Instantiate(_bomb, _bombHole.position, _bombHole.rotation);
        //    Rigidbody _bombRb = _bomb.GetComponent<Rigidbody>();
        //    _bombRb.AddForce(_bombHole.forward * _velocity, ForceMode.VelocityChange);
            
        //}
        Inputs();
        Look();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    //input bilgilerini toplar
    void Inputs()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    //temel hareket sistemi
    void Movement()
    {
        _rb.MovePosition(transform.position + (transform.forward*_input.magnitude) * _speed * Time.deltaTime);
    }
    //farkl� y�nlere d�n��� input girdisine g�re y ekseni baz al�narak forward y�n de�i�imi
    void Look()
    {
        if (_input != Vector3.zero)
        {
            var _relativeDir = (transform.position + _input) - transform.position;
            var _rot = Quaternion.LookRotation(_relativeDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, _turnSpeed * Time.deltaTime);
        }
    }
    //isometric camera i�in yukar� gitmek left+up hissiyat� verdi�i i�in; bu konuda movement g�ncellemesi yap�lacak.
    void BombInstantiate()
    {
       


    }
}

