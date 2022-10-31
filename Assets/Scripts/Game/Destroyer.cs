using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Part part) || collision.gameObject.TryGetComponent(out Item item) || collision.gameObject.TryGetComponent(out ConstituentPart constituentPart))
        {
            Destroy(collision.gameObject);
        }
    }
}
