using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Objects.UI
{
    public class HUDScreen : MonoBehaviour
    {
        public Text Coins;
        public Slider Progress;
        public Text Score;
        public Text Level1;
        public Text Level2;
        public Image LevelLayout1; 
        public Image LevelLayout2; 
        public Button Pause;

        public void FullProgress()
        {
            LevelLayout2.color = LevelLayout1.color;
            Level2.color = Level1.color; 
        }
    }
}
