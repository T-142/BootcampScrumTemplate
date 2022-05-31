using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private int health = 3;

    [SerializeField] GameObject[] _healthUI;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _fadePanel;

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.CompareTag("Enemy")) {

            health--;
            _healthUI[health].gameObject.SetActive(false);
            if (health == 0) 
            {
                _gameOver.SetActive(true);
                _fadePanel.SetActive(true);
                StartCoroutine(Fade());
            }
        }
    }

    IEnumerator Fade() 
    {
        yield return new WaitForSeconds(1.6f);
        Time.timeScale = 0;
    }
}
