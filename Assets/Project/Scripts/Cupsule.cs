using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Cupsule : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(
            origin: transform.position,
            direction: Vector3.down,
            hitInfo: out RaycastHit hit,
            maxDistance: raycastDistance,
            layerMask: layerMask
        );

        Debug.DrawRay(
            start: transform.position,
            dir: Vector3.down * raycastDistance,
            color: Color.red
        );

        if(hit.collider != null)
        {
            Debug.Log($"衝突情報 ----" +
                $"\nName = {hit.transform.name}" +
                $"\nPoint(衝突位置): {hit.point}" +
                $"\nNormal(衝突法線): {hit.normal}" +
                $"\nDistance(衝突距離): {hit.distance}");
        }

        Debug.DrawRay(
            start: hit.point,
            dir: hit.normal,
            color: Color.yellow
        );

        // 前進
        Vector3 velocity = transform.forward;

        // 進行方向と法線の外積を計算
        Vector3 right = Vector3.Cross(velocity, hit.normal);

        // 右向きベクトルと法線の外積を計算
        Vector3 slope = Vector3.Cross(right, hit.normal);

        // UnityのVector3.ProjectOnPlaneを使うと同じものが計算できる
        //Vector3 slope = Vector3.ProjectOnPlane(velocity, hit.normal);

        Debug.DrawRay(
            start: hit.point,
            dir: slope.normalized,
            color: Color.blue
        );

        transform.position += (slope.normalized * -1) * moveSpeed * Time.deltaTime;
    }
}
