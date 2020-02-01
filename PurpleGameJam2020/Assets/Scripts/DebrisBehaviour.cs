using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    private Transform target;
    private bool isColliding = false;
    private float speed = 50;

    /// <summary>
    /// Debris collection behaviour
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Player" || collider.gameObject.tag == "Player2") && !isColliding)
        {
            var ivm = collider.gameObject.GetComponent<InventoryManager>();
            if (ivm.hasReachedMaxInventory())
            {
                return;
            }
            ivm.AddDebrisCount(this.gameObject);
            GetComponent<SphereCollider>().enabled = false;
            target = collider.gameObject.transform;
            isColliding = true;
        }
    }

    public void MoveToPlayerCenter(Collider playerCollider)
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerCollider.transform.position, speed * Time.deltaTime);
    }

    public void Update()
    {
        if (target != null && isColliding)
        {
            MoveToPlayerCenter(target.GetComponent<Collider>());
            if (gameObject.transform.position == target.position)
            {
                Destroy(gameObject);
            }
        }
    }
}
