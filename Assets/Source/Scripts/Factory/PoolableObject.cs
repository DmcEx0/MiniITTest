using System;
using UnityEngine;

namespace MiniIT.MergeTwo.Factory
{
    public class PoolableObject : MonoBehaviour
    {
        public Action<PoolableObject> Disabled { get; set; }

        public void Disable()
        {
            Disabled?.Invoke(this);
            OnDisabled();
        }

        protected virtual void OnDisabled() { }
    }
}
