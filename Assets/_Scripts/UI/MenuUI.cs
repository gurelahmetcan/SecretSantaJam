using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SantaProject
{
    public class MenuUI : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnExitPressed()
        {
            Application.Quit();
        }
    }

}
