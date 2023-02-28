using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromForce : MonoBehaviour
{
    [SerializeField] private float Directionangle;
    [SerializeField] private float ForceMagnetude;    

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Directionangle, 90f, 0f)*ForceMagnetude, ForceMode.Force);
    }
}
