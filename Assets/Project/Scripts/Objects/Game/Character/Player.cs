using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Player : Character
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            var coin = col.gameObject.GetComponent<Coin>();
            if (coin != null)
            {
                coin.IsCollisionEnter = true; 
            }
        }
    }
}
