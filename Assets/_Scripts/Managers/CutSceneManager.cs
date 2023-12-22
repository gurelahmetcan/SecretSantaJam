using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SantaProject
{
    public class CutSceneManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private bool isAnimationOver;
        private float animTime;
        private float offSet = 1f;

        private void Start()
        {
            var clips = animator.runtimeAnimatorController.animationClips.ToList();
            var flyClip = clips.Find(x => x.name == "flyby");
            animTime = flyClip.length * 4 + offSet;
        }

        void Update()
        {
            animTime -= Time.deltaTime;

            if (animTime <= 0f)
            {
                isAnimationOver = true;
            }
            
            if (isAnimationOver)
            {
                isAnimationOver = false;
                ChangeScene();
            }
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
