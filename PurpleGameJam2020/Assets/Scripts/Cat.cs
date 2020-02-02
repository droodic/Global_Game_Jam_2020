using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] RepairableBehaviour[] destructables;
    int destroyIndex = 0;
    int previousIndex = 0;
    //List<RepairableBehaviour> destructableObjects = new List<RepairableBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CatAttack", 6f, 7f);
    }

    void CatAttack()
    {
        StartCoroutine(WaitAndMove(0.14f));

        previousIndex = destroyIndex;
        destroyIndex = Random.Range(0, destructables.Length);
  
        do
        {
            destroyIndex = Random.Range(0, destructables.Length);
            Debug.Log("Same index as previous, rerolling");
        }   while (destroyIndex.Equals(previousIndex));
        //transform.LookAt(Vector3(otherObject.position.x, transform.position.y, otherObject.position.z));
        this.transform.LookAt(new Vector3(destructables[destroyIndex].transform.position.x, transform.position.y, destructables[destroyIndex].transform.position.z));
    }


    IEnumerator WaitAndMove(float delayTime)
    {

        var speed = .775193f;
        var lerpValue = 0f;
        var location = this.transform.position;
        anim.SetTrigger("catAttack");
        yield return new WaitForSeconds(delayTime);


        while (true)
        {

            lerpValue += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(location, destructables[destroyIndex].JumpLocation.position, lerpValue);
            yield return null;
            if(lerpValue >= 1)
            {
                break;
            }
        }

        destructables[destroyIndex].Break();
        //destroyIndex++;
        Debug.Log("Cat attack");
    }
}
