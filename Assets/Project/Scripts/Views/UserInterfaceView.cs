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
        private readonly UIMessage<int> _leftClick = new UIMessage<int>(); 
        private readonly UIMessage<int>  _rightClick = new UIMessage<int>();
        private readonly UIMessage _pause = new UIMessage();
        private readonly UIMessage _newGame = new UIMessage();
        private readonly UIMessage _continue = new UIMessage(); 

        public UIMessage<int> IsLeftPressed => _leftClick;
        public UIMessage<int> IsRightPressed => _rightClick;
        public bool IsPausePressed => _pause.TryGet();
        public bool NewGame => _newGame.TryGet();
        public bool IsContinuePressed => _continue.TryGet(); 

        public UserInterfaceView(Screens screens)
        {
            _screens = screens; 
            

            _screens.Pause.Continue.onClick.AddListener(_continue.Set);
        }


        public void Update(int coins, int score, int level, ApplicationState state)
        {
            _screens.HUD.Coins.text = coins.ToString(); 
            
            if(_screens.InputField.Left.IsWalking)
                _leftClick.Set(1);
            
            if(_screens.InputField.Left.IsRunning)
                _leftClick.Set(2);
            
            if(_screens.InputField.Right.IsWalking)
                _rightClick.Set(1);
            
            if(_screens.InputField.Right.IsRunning)
                _rightClick.Set(2);
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
