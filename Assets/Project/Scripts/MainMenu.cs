using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button Play;

        private void Awake()
        {
            Play.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
    }
}
