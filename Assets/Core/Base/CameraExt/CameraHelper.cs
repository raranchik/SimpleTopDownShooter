using UnityEngine;

namespace Core.Base.CameraExt
{
    public class CameraHelper
    {
        public float CalculateOrthographicSize(float referenceOrthographicSize, Vector2Int referenceResolution,
            float matchWidthOrHeight, float aspect)
        {
            var targetAspect = referenceResolution.x / (float)referenceResolution.y;
            var hSize = referenceOrthographicSize * (targetAspect / aspect);
            var vLog = Mathf.Log(referenceOrthographicSize, 2);
            var hLog = Mathf.Log(hSize, 2);
            var logWeightedAverage = Mathf.Lerp(hLog, vLog, matchWidthOrHeight);
            return Mathf.Pow(2, logWeightedAverage);
        }

        public Vector3 ViewportToWorldPoint(Camera camera, Vector3 position)
        {
            var result = camera.ViewportToWorldPoint(position);
            result.z = 0f;
            return result;
        }

        public Vector3 ScreenToWorldPoint(Camera camera, Vector3 position)
        {
            var result = camera.ScreenToWorldPoint(position);
            result.z = 0f;
            return result;
        }
    }
}