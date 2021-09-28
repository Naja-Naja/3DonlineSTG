using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject cameraobj;
    [SerializeField] VisualEffect fire;
    [SerializeField] weapon weapon;
    private int playernum;
    void Start()
    {
        if (photonView.IsMine)
        {
            weapon.init();
            //StartCoroutine("bulletshoot");
            fire.Stop();
            playernum = PhotonNetwork.LocalPlayer.ActorNumber;
        }
    }
    //IEnumerator bulletshoot()
    //{

    //    while (true)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            //カメラの中心から奥へのレイを作成して発射
    //            //TODO raycastが自分にも当たってるっぽいからそのうちレイヤー付けましょう
    //            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
    //            Ray ray = Camera.main.ScreenPointToRay(center);
    //            RaycastHit hit;
    //            Physics.Raycast(ray, out hit, Mathf.Infinity);
    //            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);

    //            //プレイヤーからRayの当たった座標へのVector3を作成
    //            Vector3 playerVec = this.transform.position;
    //            Vector3 targetVec = hit.point;
    //            Vector3 bulletVec = targetVec - playerVec;
    //            //Debug.Log("playerVec=" + playerVec + "hit.point=" + hit.point);

    //            //RPCを用いてプレイヤー間で発射タイミングと方向を同期する
    //            photonView.RPC(nameof(shot), RpcTarget.All, bulletVec, playernum);
    //            //発射間隔の時間だけ待機する
    //            yield return new WaitForSeconds(1f);
    //        }
    //        yield return null;
    //    }

    //}
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                Ray ray = Camera.main.ScreenPointToRay(center);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity);
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);

                //プレイヤーからRayの当たった座標へのVector3を作成
                Vector3 playerVec = this.transform.position;
                Vector3 targetVec = hit.point;
                Vector3 bulletVec = targetVec - playerVec;
                //Debug.Log("playerVec=" + playerVec + "hit.point=" + hit.point);
                if (weapon.Shot() == null) { return; }
                //RPCを用いてプレイヤー間で発射タイミングと方向を同期する
                photonView.RPC(nameof(shot), RpcTarget.All, bulletVec, playernum);
            }
        }
    }
    //射撃同期の関数
    [PunRPC]
    private void shot(Vector3 target,int playernumber)
    {
        //弾の発射処理
        var bulletobj = Instantiate(bullet, this.transform);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODOあんまうまくいってない気がする
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target,playernumber);
    }
}
