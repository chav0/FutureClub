using System;
using Project.Scripts.Models;
using Project.Scripts.Objects;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
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
        public Settings Settings; 
        
        public void Start()
        {
            IGameplayView gameplayView = new GamePlayView(Player, Level, Settings);
            IApplicationModel model = new ApplicationModel();
            IUserInterfaceView uiView = new UserInterfaceView(Screens, Settings);
            _presenter = new ApplicationPresenter(gameplayView, uiView, model);
        }

        public void FixedUpdate()
        {
            _presenter.Update();
        }
    }

    [Serializable]
    public class Settings
    {
        public float DayDuration;
        public Gradient DayColors; 
    }
}