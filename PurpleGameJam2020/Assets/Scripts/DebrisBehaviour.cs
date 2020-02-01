using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehaviour : MonoBehaviour
{
    private Player target;
    private bool isColliding = false;
    [SerializeField] private float movementSpeed = 1.0f;
    private float lerpValue = 0.0f;
    /// <summary>
    /// Debris collection behaviour
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Player" && !isColliding) || (collider.gameObject.tag == "Player2" && !isColliding))
        {
            var ivm = collider.gameObject.GetComponent<InventoryManager>();
            if (ivm.hasReachedMaxInventory())
            {
                return;
            }
            ivm.AddDebrisCount(this.gameObject);
            UIManager.Instance.UpdateDebrisUI();
            GetComponent<SphereCollider>().enabled = false;
            
            target = collider.gameObject.GetComponent<Player>();
            isColliding = true;
        }
    }

    public void MoveToPlayerCenter(GameObject playerCollider)
    {
        lerpValue += movementSpeed * Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerCollider.transform.position, lerpValue);
    }

    public void Update()
    {
        if (target != null && isColliding)
        {
            MoveToPlayerCenter(target.gameObject);
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
