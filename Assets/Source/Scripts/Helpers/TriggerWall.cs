using MiniIT.Factory;
using UnityEngine;

namespace MiniIT
{
    public class TriggerWall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PoolableObject poolable))
            {
                poolable.Disable();
            }
        }
    }
}
