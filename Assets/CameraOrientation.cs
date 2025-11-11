using JetBrains.Annotations;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public class CameraOrientation : MonoBehaviour
{
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObject;
    [SerializeField] Rigidbody _rigidbody;

    [SerializeField] DisableObject disable;

    [SerializeField] float _rotationSpeed;

    private bool isActive = true;

    private bool shooterMode;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isActive)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            disable.Activate();
        }

        // rotate orientation
        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

  
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                _playerObject.forward = Vector3.Slerp(_playerObject.forward, inputDir.normalized, Time.deltaTime * _rotationSpeed);
        }

    // --- Fonctions de gestion d’état ---
    public void activate() => isActive = true;

    public void Disable()
    {
        isActive = false;
    }
}
