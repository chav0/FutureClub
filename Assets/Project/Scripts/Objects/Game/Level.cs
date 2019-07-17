using Project.Scripts.Objects.Game.Character;
using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Level : MonoBehaviour
    { 
        public Transform FirstPlan;
        public Transform BackPlan;

        
        
        public void Move(Vector3 position)
        {
            FirstPlan.position = new Vector3(- 2 * position.x, 0f, 0f);
            BackPlan.position = new Vector3(position.x * 0.9f, 0f, 0f);
        } 
    }
}
