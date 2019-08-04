using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Views.UserInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game
{
    public class Place : MonoBehaviour
    {
        [SerializeField] private GameObject _buyButtons; 
        [SerializeField] private GameObject _buttonsActive;
        [SerializeField] private TextMeshProUGUI _towerCost;
        [SerializeField] private TextMeshProUGUI _fenceCost;
        [SerializeField] private TextMeshProUGUI _ridgeCost;
        [SerializeField] private TextMeshProUGUI _returnCost; 
        public GameObject Canvas; 
        public Button TowerButton;
        public Button FenceButton;
        public Button RidgeButton;
        public Button DestroyButton; 
        
        public UIMessage Destroy = new UIMessage();
        public UIMessage<int> SpendMoney = new UIMessage<int>();

        private bool _isCollisionEnter;
        
        public bool IsCollisionEnter
        {
            get => _isCollisionEnter;
            set
            {
                if (_isCollisionEnter != value)
                {
                    Canvas.SetActive(value);
                    _isCollisionEnter = value; 
                }
            }
        }

        public PlaceState State { get; set; }
        public int ReturnCost { get; set; }
        
        public Tower Tower;

        private void Awake()
        {
            _towerCost.text = Tower.Cost.ToString(); 
            
            DestroyButton.onClick.AddListener(() =>
            {
                Destroy.Set();
                Tower.gameObject.SetActive(false);
                State = PlaceState.Empty; 
                _buyButtons.SetActive(true);
                _buttonsActive.SetActive(false);
            });
            
            TowerButton.onClick.AddListener(() =>
            {
                SpendMoney.Set(Tower.Cost);
                ReturnCost = Tower.Cost / 2;
                _returnCost.text = "+ " + ReturnCost; 
                Tower.gameObject.SetActive(true);
                State = PlaceState.Tower; 
                _buyButtons.SetActive(false);
                _buttonsActive.SetActive(true);
            });
        }
    }

    public enum PlaceState
    {
        Empty,
        Tower,
        Fence,
        Ridge,
    } 
}
