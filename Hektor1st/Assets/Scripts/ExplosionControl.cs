using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{


public class ExplosionControl : MonoBehaviour
{
   
        [SerializeField] private float _trigger = 0.4f;
    
        [SerializeField] private float _radius = 2.0f;
    
        [SerializeField] private float _force = 500.0f;


        private void Awake()
        {

        }
        private void Update()
        {

        }
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
                if (collision.gameObject.tag == "Shootable")
                {
                    EnemyHealth damage = collision.collider.GetComponent<EnemyHealth>();
                    damage.TakeDamage(50, transform.position);
                }
       
            }
       
            Destroy(gameObject);
    
        }
    }
}