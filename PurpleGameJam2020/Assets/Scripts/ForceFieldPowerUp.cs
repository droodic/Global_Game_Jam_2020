using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldPowerUp : MonoBehaviour
{
    int activatorPlayer = 0;
    //public ForceFieldPowerUp(int activatorPlayer)
    //{
    //    if (activatorPlayer == 0 || activatorPlayer == 1)
    //    {
    //        this.activatorPlayer = activatorPlayer;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject == PlayerManager.Instance.Players[0] && activatorPlayer == 0)
    //    {
    //        Physics.IgnoreCollision(collision.gameObject.GetComponent<SphereCollider>(), GetComponent<MeshCollider>());
    //    }
    //    //if (collision.gameObject == PlayerManager.Instance.Players[1])
    //    //{
    //    //    Physics.IgnoreCollision(PlayerManager.Instance.Players[1].GetComponent<SphereCollider>(), GetComponent<MeshCollider>());
    //    //}
    //}
}
