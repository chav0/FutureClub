using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Coin : MonoBehaviour
    {
        public int Value;
        public bool IsCollisionEnter;

        public void Realise()
        {
            Destroy(gameObject);
        }
    }
}
