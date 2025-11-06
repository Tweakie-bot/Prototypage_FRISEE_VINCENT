using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject Heal;
    [SerializeField] private Transform respawn;

    private PlayerSize playerSize;
    private Bougie BougieScript;
    void Start()
    {
        playerSize = gameObject.GetComponent<PlayerSize>();
        BougieScript = Heal.GetComponentInParent<Bougie>();
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
                respawn.position = other.transform.position;
            }

            playerSize.SetIsShrinking(false);
        }

        if (other.CompareTag("Brasier")) 
        {
            playerSize.SetIsShrinking(false);
            SceneManager.LoadScene("New Scene");
        }

        if (other.CompareTag("Obstacle"))
        {
            if (other.GetComponentInParent<Obstacle>() != null)
            {
                Obstacle obstacle = other.GetComponentInParent<Obstacle>();

                if (obstacle.GetIsBurning() == false)
                {
                    obstacle.SetBurning();
                }
            }
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

        if (other.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.GetComponentInParent<Obstacle>();

            if (obstacle.GetIsBurning() == true)
            {
                playerSize.GrowBack();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bougie"))
        {
            playerSize.SetIsShrinking(true);
        }
        if (other.CompareTag("Obstacle"))
        {
            playerSize.SetIsShrinking(true);
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = respawn.position;
        playerSize.ResetSize();
    }

    public Transform GetRespawn()
    {
        return respawn;
    }
    void Update()
    {
    }
}
