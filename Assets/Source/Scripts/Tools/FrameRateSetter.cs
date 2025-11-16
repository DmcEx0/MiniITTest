using UnityEngine;

namespace MiniIT.Tools
{
    public class FrameRateSetter : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
