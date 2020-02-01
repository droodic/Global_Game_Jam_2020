using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] RepairableBehaviour[] destructables;
    int destroyIndex = 0;
    
    //List<RepairableBehaviour> destructableObjects = new List<RepairableBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CatAttack", 6f, 7f);
    }

    void CatAttack()
    {
       
        StartCoroutine(WaitAndMove(0.14f));
    }


    IEnumerator WaitAndMove(float delayTime)
    {
       // yield return new WaitForSeconds(delayTime); // start at time X
       // float startTime = Time.time; // Time.time contains current frame time, so remember starting point

        var speed = .775193f;
        var lerpValue = 0f;
        var location = this.transform.position;
        this.transform.LookAt(destructables[destroyIndex].transform);
        anim.SetTrigger("catAttack");
        yield return new WaitForSeconds(delayTime);


        while (true)
        {

            lerpValue += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(location, destructables[destroyIndex].JumpLocation.position, lerpValue);
            //Time.time - startTime <= 1)
            yield return null;
            if(lerpValue >= 1)
            {
                break;
            }
        }

        destructables[destroyIndex].Break();
        destroyIndex++;
        Debug.Log("Cat attack");
    }
}
