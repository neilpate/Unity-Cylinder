using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField]
    float Gravity = -10f;
    
    public void Attract(Transform body, Rigidbody rb)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;

        body.rotation = Quaternion.FromToRotation(body.up, targetDir) * body.rotation;

        rb.AddForce(targetDir * Gravity);


    }
}
