using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject muzzle;
    public GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        if (muzzle != null)
        {
            var muzzleVFX = Instantiate(muzzle, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void OnCollisionEnter(Collision co)
    {
        speed = 0;
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (hit != null)
        {
            var hitfvx = Instantiate(hit, pos, rot);
            var psHit = hitfvx.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitfvx, psHit.main.duration);
            }
            else
            {
                var psChild = hitfvx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitfvx, psChild.main.duration);
            }
        }
        Destroy(gameObject);
    }
}
