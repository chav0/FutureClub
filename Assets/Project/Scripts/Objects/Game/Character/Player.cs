using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Player : Character
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            var coin = col.gameObject.GetComponent<Coin>();
            
            if(coin != null)
                coin.IsCollisionEnter = true;

            var tower = col.gameObject.GetComponent<Place>();
            
            if(tower != null)
                tower.IsCollisionEnter = true;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var tower = col.gameObject.GetComponent<Place>();
            
            if(tower != null)
                tower.IsCollisionEnter = false;
        }
    }
}
