using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject bougie;
    [SerializeField] private GameObject canvas;

    private PlayerSize playerSize;
    private Bougie BougieScript;
    private UIManager uiManager;

    private Vector3 respawn;
    void Start()
    {
        playerSize = gameObject.GetComponentInParent<PlayerSize>();
        BougieScript = bougie.GetComponent<Bougie>();
        uiManager = canvas.GetComponent<UIManager>();

        respawn = transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bougie"))
        {
            BougieScript = other.gameObject.GetComponent<Bougie>();

            if ( BougieScript == null)
            {
                BougieScript = other.gameObject.GetComponentInParent<Bougie>();
            }

            if (BougieScript.GetIsLighted() == false)
            {
                BougieScript.SetLighted();
                respawn = other.transform.position;
            }

            playerSize.SetIsShrinking(false);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bougie"))
        {
            BougieScript = other.gameObject.GetComponent<Bougie>();

            if (BougieScript == null)
            {
                BougieScript = other.gameObject.GetComponentInParent<Bougie>();
            }

            if (BougieScript != null )
            {
                if (BougieScript.GetIsLighted())
                {
                    playerSize.GrowBack();
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bougie"))
        {
            playerSize.SetIsShrinking(true);
        }
    }

    public void Respawn()
    {
        transform.position = respawn;
        playerSize.ResetSize();
        playerSize.SetIsShrinking(true);
    }
    void Update()
    {
        
    }
}
