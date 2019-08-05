using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Seeds : MonoBehaviour
    {
        public int Value;
        public bool IsCollisionEnter { get; set; }

        public void Realise()
        {
            Destroy(gameObject);
        }
    }
}