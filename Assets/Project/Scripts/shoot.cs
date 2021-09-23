using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class shoot : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletmother;
    [SerializeField] GameObject cameraobj;
    [SerializeField] VisualEffect fire;
    void Start()
    {
        if (photonView.IsMine)
        {
            bulletmother = GameObject.Find("bulletmother");
            StartCoroutine("bulletshoot");

            fire.Stop();
        }
    }
    IEnumerator bulletshoot()
    {
        if (photonView.IsMine)
        {
            while (true)
            {
                //raycastが自分にも当たってるっぽいからそのうちレイヤー付けましょう
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                Ray ray = Camera.main.ScreenPointToRay(center);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    //Debug.Log(hit.point);
                }
                var bulletobj = Instantiate(bullet, this.transform);
                //Vector3 OB = this.transform.position;

                //Vector3 OA = /*new Vector3(Screen.width / 2, Screen.height / 2);*/cameraobj.transform.position;
                //Vector3 AC = hit.point;
                //Vector3 AB = OB - OA;
                //Vector3 BC = AC - AB;
                //Debug.Log("OA=" + OA);
                //Debug.Log("AC=" + AC);
                Vector3 playerVec = this.transform.position;
                Vector3 targetVec = hit.point;
                //Debug.Log("playerVec=" + playerVec + "hit.point=" + hit.point);
                Vector3 tmp = targetVec - playerVec;
                bulletobj.transform.SetParent(bulletmother.transform);
                bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);
                bulletobj.GetComponent<bulletMove>().SetTargetPosition(tmp);
                fire.SendEvent("OnPlay");

                //発射間隔の時間だけ待機する
                yield return new WaitForSeconds(1f);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);
            }
        }
    }
}
