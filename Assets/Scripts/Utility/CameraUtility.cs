using UnityEngine;

namespace Utility
{
    public static class CameraUtility
    {
        private static readonly Camera mainCamera;

        static CameraUtility()
        {
            mainCamera = Camera.main;
        }

        public static Vector3 ScreenToWorldPoint(Vector3 screenPosition)
        {
            return mainCamera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 WorldToScreenPoint(Vector3 worldPosition)
        {
            return mainCamera.WorldToScreenPoint(worldPosition);
        }
    }
}