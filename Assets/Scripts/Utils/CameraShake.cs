using UnityEngine;

namespace Utils
{
    public class CameraShake : MonoBehaviour
    {
        // Transform of the camera to shake. Grabs the gameObject's transform
        // if null.
        private static CameraShake _instance;

        private float _originalShakeAmount;
        private float _timeSinceLastShake;
        private float _interval = 2f;
        public Transform camTransform;

        // How long the object should shake for.
        public float durationToAdd = 0.1f;
        public float shakeDuration = 0f;

        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;

        Vector3 originalPos;

        void Awake()
        {
            _instance = this;
            _originalShakeAmount = shakeAmount;
            if (camTransform == null)
            {
                camTransform = GetComponent(typeof(Transform)) as Transform;
            }

        }

        void OnEnable()
        {
            originalPos = camTransform.localPosition;
        }

        void Update()
        {
            _timeSinceLastShake -= Time.deltaTime;
            if (shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                shakeAmount = _originalShakeAmount;
                //camTransform.localPosition = originalPos;
            }
        }

        public static void ShakeWithForce(float shakeAmount, float duration)
        {
            _instance.originalPos = _instance.camTransform.localPosition;
            _instance.shakeDuration = duration;
            _instance.shakeAmount = shakeAmount;
        }
    }
}