using UIFramework.Runtime.Core;
using UnityEngine;

namespace UIFramework.Runtime.Mono
{
    [DisallowMultipleComponent]
    public class UILauncher : MonoBehaviour
    {
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            TUIManager.UpdateAllPanels();
        }
    }
}