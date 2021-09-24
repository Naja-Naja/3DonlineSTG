using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    [SerializeField] GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        //AddForce‚Åtarget‚ÉŒü‚©‚Á‚Ä”ò‚Ô
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);
    }

    //¶¬‚É“n‚³‚ê‚é”­Ë•ûŒü
    public void SetTargetPosition(Vector3 vector3)
    {
        target = vector3;
    }

    //’…’e‚É”š”­‚µ‚ÄÁ‚¦‚é
    private void OnTriggerEnter(Collider other)
    {
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(transform.root);
        Destroy(this.gameObject);
    }
}
