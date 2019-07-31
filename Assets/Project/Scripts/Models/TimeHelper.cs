using UnityEngine;

namespace Project.Scripts.Models
{
    public static class TimeHelper
    {
        private static int _currentTick;
        public static int CurrentTick => _currentTick;

        public static void Update()
        {
            _currentTick++; 
        }
    }
}
