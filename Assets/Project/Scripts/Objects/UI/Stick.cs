using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Objects.UI
{
    public class Stick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool IsWalking { get; private set; }
        public bool IsRunning { get; private set; }

        private DateTimeOffset _timePointerDown; 

        public void OnPointerDown(PointerEventData eventData)
        {
            if ((DateTime.Now - _timePointerDown).TotalMilliseconds < 500f)
                IsRunning = true;
            else
                IsWalking = true;

            _timePointerDown = DateTime.Now; 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsWalking = false; 
            IsRunning = false; 
        }
    }
}
