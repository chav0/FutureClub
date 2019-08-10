using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.Objects.UI
{
    public class Screens : MonoBehaviour
    {
        public HUDScreen HUD;
        public InputField InputField;
        public Pause Pause;
        public GameObject Victory;
        public GameObject Defeat;
        public Image DayColor;

        [SerializeField] private Button Continue;
        [SerializeField] private Button Reload1;
        [SerializeField] private Button Reload2; 

        private void Awake()
        {
            Continue.onClick.AddListener(() => Time.timeScale = 1f);
            Reload1.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}
