using Project.Scripts.Models;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts.Presenters
{
    public class ApplicationPresenter : IApplicationPresenter
    {
        private readonly IGameplayView _gameplayView;
        private readonly IUserInterfaceView _interfaceView;
        private readonly IApplicationModel _model;
        private ApplicationState _state; 

        public ApplicationPresenter(IGameplayView gameplayView, IUserInterfaceView interfaceView,
            IApplicationModel model)
        {
            _gameplayView = gameplayView;
            _interfaceView = interfaceView;
            _model = model;
            _state = ApplicationState.Game; 
            
            _interfaceView.Init(_gameplayView.Level);
        }
        
        public void Update()
        {
            _gameplayView.Update(_state);
            _model.Update();
            _model.Coins = _gameplayView.Coins; 
            _interfaceView.Update(_model.Coins, _gameplayView.Seeds, _gameplayView.Food, 0, 1, _state);
            _interfaceView.Update(_gameplayView.Level, _gameplayView.Player, _gameplayView.Enemies);

            if (_interfaceView.IsContinuePressed)
                _state = ApplicationState.UnlimitedGame; 
            
            if (_state == ApplicationState.Game || _state == ApplicationState.UnlimitedGame)
            {
                Time.timeScale = 1f; 
                if (_gameplayView.IsVictory && _state == ApplicationState.Game)
                {
                    _state = ApplicationState.Results; 
                    _interfaceView.ShowVictory();
                }else if (_gameplayView.IsGameOver)
                {
                    _state = ApplicationState.Results; 
                    _interfaceView.ShowDefeat();
                }
                else if (_interfaceView.IsLeftPressed.TryGet(out var multiplier))
                {
                    _gameplayView.SetDirectionOfPress(Direction.Left, multiplier);
                }
                else if (_interfaceView.IsRightPressed.TryGet(out multiplier))
                {
                    _gameplayView.SetDirectionOfPress(Direction.Right, multiplier);
                }
                else
                {
                    _gameplayView.SetDirectionOfPress(Direction.None, 0);
                }
            } 
            else if (_state == ApplicationState.Results)
            {
                Time.timeScale = 0f; 
            }
        }

        public void FixedUpdate()
        {
            _gameplayView.FixedUpdate(_state);
            TimeHelper.Update();
        }
    }

    public enum ApplicationState
    {
        Game,
        UnlimitedGame,
        Menu,
        Results,
    }
}