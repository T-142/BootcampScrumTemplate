using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class BombControl : MonoBehaviour
    {
        public int damagePerShot = 10;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.8f;        // The time between each shot.
        public float range = 50.0f;                      // The distance the gun can fire.

        public GameObject _projectilePrefab;
        public Transform _hole;


        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.

        Animator anim;


        void Awake()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask("Shootable");

            // Set up the references.
            anim = GetComponentInParent<Animator>();
            //faceLight = GetComponentInChildren<Light> ();

        }


        void Update()
        {

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the Fire1 button is being press and it's time to fire...
            if (Input.GetMouseButton(1) && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot();
                anim.SetBool("Fire", true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("Fire", false);
            }

        }
        void Shoot()
        {
            // Reset the timer.
            timer = 0f;

            GameObject _projectile = Instantiate(_projectilePrefab, _hole.position, _hole.rotation);
            _projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1000));
            Destroy(_projectile, 1.0f);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
            }
        }
        
    }
}