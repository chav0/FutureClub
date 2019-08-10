using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Objects;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
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
        private Settings _settings;

        public UIMessage<int> IsLeftPressed => _leftClick;
        public UIMessage<int> IsRightPressed => _rightClick;
        public bool IsPausePressed => _pause.TryGet();
        public bool NewGame => _newGame.TryGet();
        public bool IsContinuePressed => _continue.TryGet();

        public UserInterfaceView(Screens screens, Settings settings)
        {
            _screens = screens;
            _settings = settings; 

            _screens.Continue.onClick.AddListener(() =>
            {
                _continue.Set();
                _screens.Victory.gameObject.SetActive(false);
            });
        }
        
        public void Init(Level level)
        {
            _screens.HUD.Indicators.Init(level.MainBuilding, level.Portals.Select(x => x.transform).ToList(), level.Places.Select(x => x.transform).ToList());
        }

        public void Update(Level level, Player player, List<Enemy> enemies)
        {
            _screens.HUD.Indicators.UpdatePlayerPosition(player.Transform);
            _screens.HUD.Indicators.UpdatePlaces(level.Places);
            _screens.HUD.Indicators.UpdateEnemies(enemies);
        }

        public void Update(int coins, int seeds, int food, int score, int level, ApplicationState state)
        {
            _screens.HUD.Coins.text = coins.ToString();
            _screens.HUD.Seeds.text = seeds.ToString(); 
            _screens.HUD.Foods.text = food.ToString(); 
            _screens.DayColor.color = _settings.DayColors.Evaluate((Time.timeSinceLevelLoad % _settings.DayDuration) / _settings.DayDuration); 
            
            if(_screens.InputField.Left.IsWalking)
                _leftClick.Set(1);
            
            if(_screens.InputField.Left.IsRunning)
                _leftClick.Set(2);
            
            if(_screens.InputField.Right.IsWalking)
                _rightClick.Set(1);
            
            if(_screens.InputField.Right.IsRunning)
                _rightClick.Set(2);
        }

        public void ShowVictory()
        {
            _screens.Victory.SetActive(true);
        }

        public void ShowDefeat()
        {
            _screens.Defeat.SetActive(true);
        }
    }
}
