using Project.Scripts.Objects.Game.Character;
using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Coin : MonoBehaviour
    {
        public int Value;
        public bool IsCollisionEnter { get; set; }

        public void Realise()
        {
            Destroy(gameObject);
        }
    }
}
