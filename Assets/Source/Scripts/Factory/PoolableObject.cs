using System;
using UnityEngine;

namespace MiniIT.Factory
{
    public abstract class PoolableObject : MonoBehaviour
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
