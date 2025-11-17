using UnityEngine;

namespace MiniIT.MergeTwo.Tools
{
    public class FrameRateSetter : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
