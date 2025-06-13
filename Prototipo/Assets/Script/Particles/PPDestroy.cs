using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RWM
{
    public class PPDestroy : MonoBehaviour
    {
        public float time = 0;
    
        void Update()
        {
        Destroy(this.gameObject, time);
        }
    }
}
