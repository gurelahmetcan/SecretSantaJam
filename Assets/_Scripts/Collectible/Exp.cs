using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class Exp : MonoBehaviour
    {
        public int experience;
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
    
}
