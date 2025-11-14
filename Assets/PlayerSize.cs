using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSize : MonoBehaviour
{
    [SerializeField] private float MinSize;
    [SerializeField] private float ShrinkRate;
    [SerializeField] private TMP_Text _textMeshPro;

    private bool isShrinking;

    private Vector3 OriginalSize;

    private PlayerInteraction playerInteraction;
    void Start()
    {
        playerInteraction = gameObject.GetComponent<PlayerInteraction>();

        OriginalSize = transform.localScale;

        isShrinking = true;

        InvokeRepeating("Shrink", 5, 2);
    }

    void Update()
    {
        if(transform.localScale.y < MinSize)
        {
            playerInteraction.Respawn();
            Debug.Log("Respawn at " + playerInteraction.GetRespawn());
        }
        _textMeshPro.text = $"VIE : {transform.localScale.y.ToString()}";
    }
    public void Shrink()
    {
        if (isShrinking)
        {
            Vector3 shrink = new Vector3(ShrinkRate, ShrinkRate, ShrinkRate);
            transform.localScale -= shrink;
        }
    }

    public void StopShrinking()
    {
        isShrinking = false;
    }
    public void ResumeS()
    {
        isShrinking = true;
    }

    public void ResetSize()
    {
        transform.localScale = OriginalSize;
    }
}
