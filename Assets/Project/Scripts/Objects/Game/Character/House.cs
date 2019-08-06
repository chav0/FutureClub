using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Views.UserInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _foodForSellUICount;
    [SerializeField] private TextMeshProUGUI _houseUpUICost;
    
    public GameObject Canvas;
    
    public Button SellButton;
    public Button HauseUpButton;
    
    public UIMessage<int> Sell = new UIMessage<int>();
    public UIMessage<int> HauseUp = new UIMessage<int>();
    
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
        
        SellButton.onClick.AddListener(() =>
        {
            Sell.Set(_foodForSellCount);
        });
            
        HauseUpButton.onClick.AddListener(() =>
        {
            HauseUp.Set(_houseUpCost);
        });
    }
}
