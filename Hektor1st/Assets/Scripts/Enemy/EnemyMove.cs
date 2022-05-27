using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    
    public class EnemyMove : MonoBehaviour
    {
        Transform player;               // Reference to the player's position.
        //PlayerHealth playerHealth;      // Reference to the player's health.
        //EnemyHealth enemyHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.

        public ParticleSystem _particle;
        void Awake ()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            //playerHealth = player.GetComponent <PlayerHealth> ();
            //enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        }


        void Update ()
        {
            // If the enemy and the player have health left...
            //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            //{
                // ... set the destination of the nav mesh agent to the player.
                nav.SetDestination (player.position);
            //}
            // Otherwise...
            //else
            //{
                // ... disable the nav mesh agent.
                //nav.enabled = false;
            //}
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                _particle.Play();
            }
        }
    }
}