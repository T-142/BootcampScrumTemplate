using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float Health = 100;
    public Image Bar;
    [SerializeField] GameObject[] _healthUI;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _fadePanel;

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Health <= 0) 
            {
                _gameOver.SetActive(true);
                _fadePanel.SetActive(true);
                StartCoroutine(Fade());
            }
            else 
            {
                Health -= 10f;
                Bar.fillAmount = Health / 100;
                //_healthUI[health].gameObject.SetActive(false);
            }
        }

    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }
}
