using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private float randomTime;
    [SerializeField] private GameObject debrisParticle;
    private bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(20f, 45f);
        Invoke("MakeFall", randomTime);
    }

    // Update is called once per frame
    void Update()
    {
        //if(hasCollided)
        //{
        //    var particle = Instantiate(debrisParticle, gameObject.transform.position, Quaternion.identity, null);
        //}
    }

    public void MakeFall()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(86.5f, 5f, 0));
    }

    public void OnCollision(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (debrisParticle != null)
            {
                hasCollided = true;
            }
        }
    }
}
