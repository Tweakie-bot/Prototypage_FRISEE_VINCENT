using UnityEngine;
public class WindBeam : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float windWidth = 0.3f;
    public LayerMask obstacleMask;

    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

        if (!line)
        {
            Debug.LogError("beamObject doit contenir un LineRenderer et un BoxCollider !");
            enabled = false;
            return;
        }

        line.positionCount = 2;
        line.startWidth = windWidth;
        line.endWidth = windWidth;
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
        if (Physics.Raycast(start, direction, out RaycastHit hit, maxDistance))
        {
            end = hit.point;

            GameObject player = hit.collider.gameObject;

            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerInteraction>().Respawn();
            }
        }

        float currentDistance = Vector3.Distance(start, end);

        // MAJ visuel
        line.SetPosition(0, start);
        line.SetPosition(1, end);
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

