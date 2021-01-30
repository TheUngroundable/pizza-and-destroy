using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisible : MonoBehaviour
{
     private void OnCollisionEnter(Collision other) 
   {
        if(other.transform.tag=="MainCamera")
        {
            GetComponent<MeshRenderer>().enabled= false;
            //StartCoroutine(LerpOut());
        }
   }

    private void OnCollisionExit(Collision other) 
   {
        if(other.transform.tag=="MainCamera")
        {
            transform.localScale = new Vector3(100,100,100);
            GetComponent<MeshRenderer>().enabled= true;
        }
   }

     private IEnumerator LerpOut()
   {
        float elapsedTime   = 0;
        Vector3 startScale = new Vector3(100,100,100);
        Vector3 toScale = Vector3.zero;
        while (elapsedTime < 0.3f)
        {
            transform.localScale = Vector3.Lerp(startScale, toScale, (elapsedTime /  0.3f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
         transform.localScale = toScale;
        GetComponent<MeshRenderer>().enabled= false;
    }


}
