using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.


        Animator anim;                          // Reference to the animator component.
        [SerializeField] Button _restart;
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _panel;
        [SerializeField] Button _quitButton;
        [SerializeField] Button _resumeButton;

        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health...
            if(playerHealth.currentHealth <= 0 || _player.transform.position.y < -5.0f)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger ("GameOver");
                _restart.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RestartGame();
                }
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                _panel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            _panel.gameObject.SetActive(false);
        }
    }
}