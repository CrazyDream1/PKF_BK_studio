using UnityEngine;

namespace Core
{ 
    public class CameraMovement : MonoBehaviour 
    {
        [SerializeField] public float MouseSensitivity = 3.0f;
        [SerializeField] public float Speed = 2.0f;

        [SerializeField] public float MinimumX = -360F;
        [SerializeField] public float MaximumX = 360F;
        [SerializeField] public float MinimumY = -60F;
        [SerializeField] public float MaximumY = 60F;

        void Awake()
        {
            GetComponent<Camera>().orthographic = false;
        }

        void Start()
        {
            _originalRotation = transform.rotation;
        }

        void Update()
        {
            if (_enabled)
            {
                _rotationX += Input.GetAxis("Mouse X") * MouseSensitivity;
                _rotationY += Input.GetAxis("Mouse Y") * MouseSensitivity;
                _rotationX = ClampAngle(_rotationX, MinimumX, MaximumX);
                _rotationY = ClampAngle(_rotationY, MinimumY, MaximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, Vector3.left);
                transform.rotation = _originalRotation * xQuaternion * yQuaternion;

                _transfer = transform.forward * Input.GetAxis("Vertical");
                _transfer += transform.right * Input.GetAxis("Horizontal");
                transform.position += _transfer * (Speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _enabled = !_enabled;
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        private bool _enabled = true;
        private Vector3 _transfer;
        private float _rotationX = 0F;
        private float _rotationY = 0F;
        private Quaternion _originalRotation;
    }
}