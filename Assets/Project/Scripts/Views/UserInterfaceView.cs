using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Objects;
using Project.Scripts.Objects.UI;
using Project.Scripts.Presenters;
using Project.Scripts.Views.UserInput;
using UnityEngine;

namespace Project.Scripts.Views
{
    public class UserInterfaceView : IUserInterfaceView
    {
        private Screens _screens;
        private readonly UIMessage _leftClick = new UIMessage(); 
        private readonly UIMessage _rightClick = new UIMessage();
        private readonly UIMessage _pause = new UIMessage();
        private readonly UIMessage _newGame = new UIMessage();
        private readonly UIMessage _continue = new UIMessage(); 

        public bool IsLeftPressed => _leftClick.TryGet();
        public bool IsRightPressed => _rightClick.TryGet();
        public bool IsPausePressed => _pause.TryGet();
        public bool NewGame => _newGame.TryGet();
        public bool IsContinuePressed => _continue.TryGet(); 

        public UserInterfaceView(Screens screens)
        {
            _screens = screens; 
            
            _screens.InputField.Left.onClick.AddListener(_leftClick.Set);
            _screens.InputField.Right.onClick.AddListener(_rightClick.Set);
            _screens.HUD.Pause.onClick.AddListener(_pause.Set);
            _screens.Pause.Continue.onClick.AddListener(_continue.Set);
        }


        public void Update(int coins, float progress, int score, int level, ApplicationState state)
        {
            if (state == ApplicationState.Game)
            {
                _screens.HUD.Coins.text = coins.ToString();
                _screens.HUD.Progress.value = progress;
                _screens.HUD.Level1.text = level.ToString();
                _screens.HUD.Level2.text = (++level).ToString();

                if (progress >= 1f)
                {
                    _screens.HUD.FullProgress();
                }
            }
        }

        public void ShowGameOver()
        {
            _screens.HUD.gameObject.SetActive(false);
            _screens.InputField.gameObject.SetActive(false);
            _screens.Pause.gameObject.SetActive(false);
        }

        public void ShowNewGame()
        {
            _screens.HUD.gameObject.SetActive(true);
            _screens.InputField.gameObject.SetActive(true);
            _screens.Pause.gameObject.SetActive(false);
        }

        public void ShowMainMenu()
        {
            _screens.HUD.gameObject.SetActive(false);
            _screens.InputField.gameObject.SetActive(false);
            _screens.Pause.gameObject.SetActive(false);
        }

        public void ShowCharSelect()
        {
            _screens.HUD.gameObject.SetActive(false);
            _screens.InputField.gameObject.SetActive(false);
            _screens.Pause.gameObject.SetActive(false);
        }

        public void ShowPause()
        {
            _screens.HUD.gameObject.SetActive(false);
            _screens.InputField.gameObject.SetActive(false);
            _screens.Pause.gameObject.SetActive(true);
        }
    }
}
