using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahtung_Script : MonoBehaviour
{

    public float Delay = 3f;
    public float Radius = 10f;
    float odlicznia;
    bool HasExploaded = false;
    public GameObject ExpolsionEffect;

    // Start is called before the first frame update
    private void Start()
    {
        odlicznia = Time.time + Delay;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(HasExploaded);
        if (!HasExploaded && Time.time > odlicznia)
        {

            Explode();
            HasExploaded = true;
        }
    }


    public void Explode()
    {      
        Collider[] coliders = Physics.OverlapSphere(transform.position, Radius);        
        foreach(Collider col in coliders)
        {
            EnemyHealth es = col.GetComponent<EnemyHealth>();
            if(es != null)
            {
                es.DoDamage(5);
            }
        }
        Destroy(gameObject, 3);
        var newEffect = Instantiate(ExpolsionEffect, transform.position, Quaternion.identity);
        Destroy(newEffect, 2);
        gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
