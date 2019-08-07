using System;
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
        [SerializeField] private TextMeshProUGUI _repairCost; 
        [SerializeField] private GameObject _empty; 
        public GameObject Canvas; 
        public Button TowerButton;
        public Button FenceButton;
        public Button RidgeButton;
        public Button DestroyButton; 
        public Button RepairButton; 
        public Button PlantButton; 
        public Button HarvestButton; 
        
        public UIMessage Destroy = new UIMessage();
        public UIMessage<int> BuyTower = new UIMessage<int>();
        public UIMessage<int> BuyFence = new UIMessage<int>();
        public UIMessage<int> BuyRidge = new UIMessage<int>();
        public UIMessage<int> SpendSeeds = new UIMessage<int>();
        public UIMessage<int> Loot = new UIMessage<int>();
        public UIMessage<int, PlaceState> Repair = new UIMessage<int, PlaceState>();

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
        public Fence Fence;
        public Ridge Ridge;

        private void Awake()
        {
            _towerCost.text = Tower.Cost.ToString();
            _fenceCost.text = Fence.Cost.ToString(); 
            _ridgeCost.text = Ridge.Cost.ToString(); 

            DestroyButton.onClick.AddListener(() =>
            {
                Destroy.Set();
                SetEmpty();
            });
            
            RepairButton.onClick.AddListener(() =>
            {
                Repair.Set(RepairCost(), State);
            });

            TowerButton.onClick.AddListener(() =>
            {
                BuyTower.Set(Tower.Cost);
            });
            
            FenceButton.onClick.AddListener(() =>
            {
                BuyFence.Set(Fence.Cost);
            });
            
            RidgeButton.onClick.AddListener(() =>
            {
                BuyRidge.Set(Ridge.Cost);
            });
            
            PlantButton.onClick.AddListener(() =>
            {
                SpendSeeds.Set(1);
            });
            
            HarvestButton.onClick.AddListener(() =>
            {
                Loot.Set(1);
                Ridge.SetEmpty();
            });
        }

        private void Update()
        {
            if (Canvas.gameObject.activeSelf && State != PlaceState.Empty)
            {
                _repairCost.text = RepairCost().ToString(); 
            }

            if (Canvas.gameObject.activeSelf && State == PlaceState.Ridge)
            {
                PlantButton.gameObject.SetActive(Ridge.State == RidgeState.Empty);
                HarvestButton.gameObject.SetActive(Ridge.State == RidgeState.Ready);
            }
        }

        private int RepairCost()
        {
            switch (State)
            {
                case PlaceState.Tower:
                    return (int) Math.Ceiling(Tower.Cost * 0.5f);
                case PlaceState.Fence:
                    return (int) Math.Ceiling(Fence.Cost * 0.5f);
                case PlaceState.Ridge:
                    return (int) Math.Ceiling(Ridge.Cost * 0.5f);
                default:
                    return 0; 
            }
        }

        public void SetEmpty()
        {
            State = PlaceState.Empty; 
            _buyButtons.SetActive(true);
            _buttonsActive.SetActive(false);
            SetState(); 
        }

        public void SetTower()
        {
            //ReturnCost = Tower.Cost / 2;
            ReturnCost = Tower.Cost;
            _returnCost.text = "+ " + ReturnCost; 
            State = PlaceState.Tower; 
            _buyButtons.SetActive(false);
            _buttonsActive.SetActive(true);
            SetState(); 
        }

        public void SetFence()
        {
            //ReturnCost = Fence.Cost / 2;
            ReturnCost = Fence.Cost;
            _returnCost.text = "+ " + ReturnCost; 
            State = PlaceState.Fence; 
            _buyButtons.SetActive(false);
            _buttonsActive.SetActive(true);
            SetState(); 
        }

        public void SetRidge()
        {
            //ReturnCost = Ridge.Cost / 2;
            ReturnCost = Ridge.Cost;
            _returnCost.text = "+ " + ReturnCost; 
            State = PlaceState.Ridge; 
            _buyButtons.SetActive(false);
            _buttonsActive.SetActive(true);
            SetState(); 
        }

        private void SetState()
        {
            Tower.gameObject.SetActive(State == PlaceState.Tower);
            Fence.gameObject.SetActive(State == PlaceState.Fence);
            PlantButton.gameObject.SetActive(State == PlaceState.Ridge);
            HarvestButton.gameObject.SetActive(false);
            Ridge.gameObject.SetActive(State == PlaceState.Ridge);
            _empty.gameObject.SetActive(State == PlaceState.Empty);
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
