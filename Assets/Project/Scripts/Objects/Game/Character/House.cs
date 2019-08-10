using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Views.UserInput;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _foodForSellUICount;
    [SerializeField] private TextMeshProUGUI _houseUpUICost;
    [SerializeField] private int _health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private SpriteRenderer _sprite;
    
    public GameObject Canvas;
    
    public Button SellButton;
    public Button HauseUpButton;
    public Health Health;
    
    public UIMessage<int> Sell = new UIMessage<int>();
    public UIMessage<int> HouseUp = new UIMessage<int>();
    
    public int _foodForSellCount;
    public int _houseUpCost;
    public int _foodCost;

    public SpriteRenderer HouseSprite;
    public Sprite GoodHouseSprite;
    
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

    private void Awake()
    {
        _foodForSellUICount.text = _foodForSellCount.ToString();
        _houseUpUICost.text = _houseUpCost.ToString(); 
        Health = new Health(_health, null, _sprite);
        
        SellButton.onClick.AddListener(() =>
        {
            Sell.Set(_foodForSellCount);
        });
            
        HauseUpButton.onClick.AddListener(() =>
        {
            HouseUp.Set(_houseUpCost);
        });
    }
    
    private void Update()
    {
        if (_healthBar != null)
        {
            _healthBar.value = Health.CurrentHealth / (float) Health.MaxHealth; 
        }
    }
}
