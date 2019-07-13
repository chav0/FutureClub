using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Level : MonoBehaviour
    {
        public float FullProgress;

        public float CurrentProgress()
        {
            return Mathf.Abs(transform.localPosition.y) / FullProgress; 
        }
    }
}
