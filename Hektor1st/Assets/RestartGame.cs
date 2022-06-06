using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] Button _restart;
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}