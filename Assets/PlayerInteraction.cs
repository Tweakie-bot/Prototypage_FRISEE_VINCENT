using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform respawn;
    [SerializeField] private Image alpha;

    private Vector3 FirstRespawn;
    private PlayerSize playerSize;
    private Bougie BougieScript;

    private List<GameObject> destroyedObject;

    private List<GameObject> _flames;

    private List<GameObject> saveDestoyedObject;

    public void AddDestroyedObject(GameObject o)
    {
        destroyedObject.Add(o);
    }
    void Start()
    {
        playerSize = gameObject.GetComponent<PlayerSize>();
        destroyedObject = new List<GameObject>();
        saveDestoyedObject = new List<GameObject>();
        FirstRespawn = new Vector3(respawn.position.x, respawn.position.y, respawn.position.z);
        _flames = new List<GameObject>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Flame"))
        {
            playerSize.ResetSize();

            _flames.Add(other.gameObject);

            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Bougie"))
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
                saveDestoyedObject = destroyedObject;
            }
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            Respawn();
        }

        if (other.gameObject.CompareTag("Brasier")) 
        {
            SceneManager.LoadScene("New Scene");
        }

        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("BurnIt"))
        {
            Burn obstacle;

            if (other.gameObject.GetComponentInParent<Burn>() == null)
            {
                obstacle = other.gameObject.GetComponent<Burn>();
            } 

            else
            {
                obstacle = other.gameObject.GetComponentInParent<Burn>();
            }

            if (obstacle.GetIsBurning() == false)
            {
                obstacle.SetBurning();
                Debug.Log($"{obstacle} {obstacle.GetIsBurning()}");
            }
        }
    }

    public void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Debug.Log("RESPAWN POSITION SET TO : " + respawn.position);

        if (respawn.position == FirstRespawn)
        {
            foreach (GameObject destroyed_object in destroyedObject)
            {
                destroyed_object.SetActive(true);

                Burn script;

                if (destroyed_object.GetComponentInParent<Burn>() == null)
                {
                    script = destroyed_object.GetComponent<Burn>();
                } else
                {
                    script = destroyed_object.GetComponentInParent<Burn>();
                }

                script.GiveHealthBack();
            }
        }
        foreach (GameObject destroyed_object in destroyedObject)
        {
            if (!saveDestoyedObject.Contains(destroyed_object))
            {
                destroyed_object.SetActive(true);

                Burn script;

                if (destroyed_object.GetComponentInParent<Burn>() == null)
                {
                    script = destroyed_object.GetComponent<Burn>();
                }
                else
                {
                    script = destroyed_object.GetComponentInParent<Burn>();
                }

                script.GiveHealthBack();
            }
        }

        foreach (GameObject destroyed in _flames)
        {
            destroyed.SetActive(true);
        }
        _flames.Clear();

        StartCoroutine(RespawnEffect());

        Debug.Log($"Respawn in Player Interaction {respawn.position}");

        transform.position = respawn.position;

        print(gameObject.name + " " +  transform.position);

        playerSize.ResetSize();

        Debug.Log("PLAYER POSITION AFTER TELEPORT : " + transform.position);

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
