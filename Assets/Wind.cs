using UnityEngine;
public class WindBeam : MonoBehaviour
{
    [Header("Références")]
    public Transform startPoint;
    public Transform endPoint;
    public Transform beamObject; // ← référence vers l’objet contenant le collider + renderer

    [Header("Paramètres")]
    public float windWidth = 0.3f;
    public LayerMask obstacleMask;

    private LineRenderer line;
    private BoxCollider boxCollider;

    private void Start()
    {
        if (!beamObject)
        {
            Debug.LogError("Assigne 'beamObject' (l’objet qui contient le collider et le LineRenderer).");
            enabled = false;
            return;
        }

        line = beamObject.GetComponent<LineRenderer>();
        boxCollider = beamObject.GetComponent<BoxCollider>();

        if (!line || !boxCollider)
        {
            Debug.LogError("beamObject doit contenir un LineRenderer et un BoxCollider !");
            enabled = false;
            return;
        }

        line.positionCount = 2;
        line.startWidth = windWidth;
        line.endWidth = windWidth;
        boxCollider.isTrigger = true;
    }

    private void Update()
    {
        UpdateWind();
    }

    private void UpdateWind()
    {
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;
        Vector3 direction = (end - start).normalized;
        float maxDistance = Vector3.Distance(start, end);

        // Raycast
        if (Physics.Raycast(start, direction, out RaycastHit hit, maxDistance, obstacleMask, QueryTriggerInteraction.Ignore))
        {
            end = hit.point;

            GameObject player = hit.collider.gameObject;

            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerSize>().ShrinkFast();
            }
        }

        float currentDistance = Vector3.Distance(start, end);

        // MAJ visuel
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // Aligne uniquement beamObject (pas tout le vent)
        beamObject.position = start;
        beamObject.rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Ajuste le collider
        boxCollider.size = new Vector3(windWidth, windWidth, currentDistance);
        boxCollider.center = new Vector3(0, 0, currentDistance / 2f);
    }

    private void OnDrawGizmos()
    {
        if (startPoint && endPoint)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}

