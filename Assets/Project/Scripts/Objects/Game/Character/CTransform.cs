using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class CTransform
    {
        public Transform Transform;
        public float Speed { get; set; }
        private Animator _animator;

        public CTransform(Transform transform, float speed, Animator animator)
        {
            Transform = transform;
            Speed = speed;
            _animator = animator; 
        }

        public void Move(Direction direction, float multiplier = 1f)
        {
            Transform.position += new Vector3(multiplier * Speed * Time.deltaTime * (int) direction, 0f, 0f);
            
            if(direction != Direction.None)
                Transform.localScale = new Vector3((int) direction, 1f, 1f);
            
            _animator.SetFloat("Blend", multiplier);
        }
    }

    public enum Direction
    {
        Left = -1,
        Right = 1,
        None = 0,
    }
}
