using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    [SerializeField] private float _trigger = 0.5f;
    [SerializeField] private float _radius = 5.0f;
    [SerializeField] private float _force = 500.0f;
    [SerializeField] private GameObject _particle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= _trigger)
        {
            var _objects = Physics.OverlapSphere(transform.position, _radius);
            foreach(var _object in _objects)
            {
                var _rb = _object.GetComponent<Rigidbody>();
                if (_rb == null)
                {
                    continue;
                }
                _rb.AddExplosionForce(_force, transform.position, _radius);
            }
        }
        Instantiate(_particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
