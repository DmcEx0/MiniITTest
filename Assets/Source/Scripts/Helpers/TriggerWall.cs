using MiniIT.MergeTwo.Factory;
using UnityEngine;

namespace MiniIT.MergeTwo.Helpers
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
