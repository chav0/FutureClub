using Project.Scripts.Models;
using Project.Scripts.Objects;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.UI;
using Project.Scripts.Presenters;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts
{
    public class Init : MonoBehaviour
    {
        private IApplicationPresenter _presenter;
        public Screens Screens;
        public Player Player;
        public Level Level; 
        
        public void Start()
        {
            IGameplayView gameplayView = new GamePlayView(Level, Player);
            IApplicationModel model = new ApplicationModel();
            IUserInterfaceView uiView = new UserInterfaceView(Screens);
            _presenter = new ApplicationPresenter(gameplayView, uiView, model);
        }

        public void Update()
        {
            _presenter.Update();
        }
    }
}