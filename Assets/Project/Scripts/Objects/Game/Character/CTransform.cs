using System;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class CTransform
    {
        public Transform Transform;
        private Canvas _canvas; 
        public float Speed { get; set; }
        private Animator _animator;
        private float _lerpedMultiplier; 
        
        public Vector3 Position => Transform.position; 

        public CTransform(Transform transform, float speed, Animator animator, Canvas canvas)
        {
            Transform = transform;
            Speed = speed;
            _canvas = canvas; 
            _animator = animator; 
        }

        public void Move(Direction direction, float multiplier = 1f)
        {
            _lerpedMultiplier = Mathf.Lerp(_lerpedMultiplier, multiplier, .1f);
            
            Transform.position += new Vector3(_lerpedMultiplier * Speed * Time.deltaTime * (int) direction, 0f, 0f);

            if (direction != Direction.None)
            {
                Transform.localScale = new Vector3((int) direction, 1f, 1f);
                
                if(_canvas != null)
                    _canvas.transform.localScale = new Vector3(_canvas.transform.localScale.y * (int) direction, _canvas.transform.localScale.y, 1f);
            }
            
            _animator.SetFloat("Blend", _lerpedMultiplier);
        }
    }

    public enum Direction
    {
        Left = -1,
        Right = 1,
        None = 0,
    }
}
