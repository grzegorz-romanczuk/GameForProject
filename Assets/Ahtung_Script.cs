using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ahtung_Script : MonoBehaviour
{

    public float Delay = 3f;
    public float Radius = 10f;
    public GameObject ExplosionEvect;
    float odlicznia;
    bool HasExploaded = false;
    // Start is called before the first frame update
    void Start()
    {
        odlicznia = Delay;   
    }

    // Update is called once per frame
    void Update()
    {
        odlicznia-=Time.deltaTime;
        if(odlicznia<= 0f && !HasExploaded)
        {
            Explode();
            HasExploaded = false;
        }
        
    }


    public void Explode()
    {

        Instantiate(ExplosionEvect, transform.position, transform.rotation);

        Collider[] coliders = Physics.OverlapSphere(transform.position, Radius);

        foreach(Collider col in coliders)
        {
            EnemyHealth es = col.GetComponent<EnemyHealth>();
            if(es != null)
            {
                es.DoDamage(5);
            }
        }
        Destroy(gameObject);
    }

}
