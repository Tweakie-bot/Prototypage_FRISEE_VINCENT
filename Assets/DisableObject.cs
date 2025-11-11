using UnityEngine;
using Unity.Cinemachine;

public class DisableObject : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _p;
    [SerializeField] private CinemachineCamera _cine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disable()
    {
        _menu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cine.enabled = true;

        Camera.main.GetComponent<CameraOrientation>().activate();

        _p.GetComponent<PlayerSize>().ResumeS();
    }

    public void Activate()
    {
        Debug.Log("Activate");

        _menu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _cine.enabled = false;

        Camera.main.GetComponent<CameraOrientation>().Disable();

        _p.GetComponent<PlayerSize>().StopShrinking();
    }
}
