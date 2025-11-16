using UnityEngine;

namespace MiniIT.Factory
{
    public class GameObjectFactory
    {
        protected T Create<T>(T prefab) where T : Object
        {
            var instance = Object.Instantiate(prefab);
            return instance;
        }
    }
}
