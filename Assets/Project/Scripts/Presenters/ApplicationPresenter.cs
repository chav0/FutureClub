using Project.Scripts.Models;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Views;

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
        }
        
        public void Update()
        {
            
            _gameplayView.Update(_state);
            _model.Update();
            _interfaceView.Update(_model.Coins + _gameplayView.CoinsCollectedInLastGame, 0, 1, _state);

            if (_state == ApplicationState.Game)
            {
                if (_gameplayView.IsGameOver)
                {
                    _interfaceView.ShowGameOver();
                    _state = ApplicationState.Results; 
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
                    if (_interfaceView.IsPausePressed)
                    {
                        _interfaceView.ShowPause();
                        _gameplayView.SetPause(true);
                        _state = ApplicationState.Pause;
                    }
                }
            } 
            else if (_state == ApplicationState.Results)
            {
                
            }
            else if (_state == ApplicationState.Menu)
            {
                if (_interfaceView.NewGame)
                {
                    _interfaceView.ShowNewGame();
                    _state = ApplicationState.Game; 
                }
            }
            else if (_state == ApplicationState.Pause)
            {
                if (_interfaceView.IsContinuePressed)
                {
                    _interfaceView.ShowNewGame();
                    _gameplayView.SetPause(false);
                    _state = ApplicationState.Game; 
                }
            }
        }
    }

    public enum ApplicationState
    {
        Game,
        Menu,
        Results,
        Pause,
        CharSelect
    }
}