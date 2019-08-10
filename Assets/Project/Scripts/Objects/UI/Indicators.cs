using System.Collections.Generic;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
using UnityEngine;

namespace Project.Scripts.Objects.UI
{
    public class Indicators : MonoBehaviour
    {
        [SerializeField] private RectTransform _home; 
        [SerializeField] private RectTransform _player; 
        [SerializeField] private RectTransform _placePrefab; 
        [SerializeField] private RectTransform _portalPerfab; 
        [SerializeField] private RectTransform _enemyPrefab;
        [SerializeField] private float _modificator;

        private List<RectTransform> _places;
        private List<RectTransform> _enemies;

        public void Init(Transform homeTransform, List<Transform> portals, List<Transform> places)
        {
            _home.anchoredPosition = new Vector2(Projection(homeTransform.position.x), _home.anchoredPosition.y);

            _places = new List<RectTransform>();
            _enemies = new List<RectTransform>();
            
            foreach (var placeTransform in places)
            {
                var icon = Instantiate(_placePrefab, transform); 
                icon.anchoredPosition = new Vector2(Projection(placeTransform.position.x), icon.anchoredPosition.y);
                _places.Add(icon);
            }
            
            foreach (var postalTransform in portals)
            {
                var icon = Instantiate(_portalPerfab, transform); 
                icon.anchoredPosition = new Vector2(Projection(postalTransform.position.x), icon.anchoredPosition.y);
                icon.gameObject.SetActive(true);
            }

            for (int i = 0; i < 20; i++)
            {
                _enemies.Add(Instantiate(_enemyPrefab, transform));
            }
        }

        public void UpdatePlayerPosition(CTransform playerTransform)
        {
            _player.anchoredPosition = new Vector2(Projection(playerTransform.Position.x), _player.anchoredPosition.y);
        }

        public void UpdatePlaces(List<Place> places)
        {
            for (int i = 0; i < places.Count; i++)
            {
                var place = places[i];
                var icon = _places[i]; 
                
                icon.gameObject.SetActive(place.State != PlaceState.Empty);
            }
        }
        
        public void UpdateEnemies(List<Enemy> enemies)
        {
            var i = 0; 
            for (; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                var icon = _enemies[i]; 
                
                icon.gameObject.SetActive(true);
                icon.anchoredPosition = new Vector2(Projection(enemy.Transform.Position.x), icon.anchoredPosition.y);
            }

            for (int j = i + 1; j < _enemies.Count; j++)
            {
                var icon = _enemies[j]; 
                icon.gameObject.SetActive(false);
            }
        }

        private float Projection(float positionX)
        {
            return positionX * _modificator; 
        }
    }
}
