using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);
    }

    // Update is called once per frame
    void Update()
    {
        //var tmp = target.normalized * 10;
        //Rigidbody.velocity = target.normalized*10*Time.deltaTime;
        //Rigidbody.AddForce(target.normalized*1000);
        //transform.position = Vector3.MoveTowards(transform.position, target, 10*Time.deltaTime);
    }
    public void SetTargetPosition(Vector3 vector3)
    {
        target = vector3;
    }
    private void OnTriggerEnter(Collider other)
    {
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(transform.root);
        //Debug.Log("hit");
        Destroy(this.gameObject);
    }
}
