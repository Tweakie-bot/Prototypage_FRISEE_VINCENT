using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject Heal;
    [SerializeField] private Transform respawn;
    [SerializeField] private Image alpha;

    private PlayerSize playerSize;
    private Bougie BougieScript;
    void Start()
    {
        playerSize = gameObject.GetComponent<PlayerSize>();
        BougieScript = Heal.GetComponentInParent<Bougie>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision détectée avec {other.name}, tag = {other.tag}");

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
                Debug.Log($"New respawn position {respawn.position}");
            }

            playerSize.SetIsShrinking(false);
        }

        if (other.CompareTag("Respawn"))
        {
            Respawn();
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
        StartCoroutine(RespawnEffect());
        //playerController.SetCanUpdate(false);

        Debug.Log($"Respawn in Player Interaction {respawn.position}");

        transform.position = respawn.position;

        print(gameObject.name + " " +  transform.position);

        playerSize.ResetSize();

    }

    public Transform GetRespawn()
    {
        return respawn;
    }
    void Update()
    {

    }

    IEnumerator RespawnEffect()
    {
        Color color_start = Color.black;
        color_start.a = 1f;
        Color color_end = Color.black;
        color_end.a = 0f;

        float t = 0f;

        alpha.color = color_start;
        while (t < 4)
        {
            t += Time.deltaTime;

            float k = t / 2;

            alpha.color = Color.Lerp(color_start, color_end, k);

            yield return null;
        }
        alpha.color = color_end;
    }
}
