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
    [SerializeField] weapon weapon;//ここをweaponflameにする
    [SerializeField] weaponflame weaponflame;
    [SerializeField] Transform testtransform;
    [SerializeField] int[] test;
    //[SerializeField]
    //private LayerMask layerMask;
    //[SerializeField] float scroll;
    private int playernum;
    void Start()
    {
        if (photonView.IsMine)
        {
            //testtransform = GameObject.Find("Cube").GetComponent<Transform>();
            for (int i = 0; i < weaponflame.Equipment.Count; i++)
            {
                weaponflame.Equipment[i].init();
            }
            fire.Stop();
            playernum = PhotonNetwork.LocalPlayer.ActorNumber;
        }
    }
    private void Update()
    {
        //攻撃系の入力を扱う
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                //直線射撃時の処理
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                Ray ray = Camera.main.ScreenPointToRay(center);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity/*,layerMask*/);
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);

                //プレイヤーからRayの当たった座標へのVector3を作成
                Vector3 playerVec = this.transform.position;
                Vector3 targetVec = hit.point;
                Vector3 bulletVec = targetVec - playerVec;
                //Debug.Log("playerVec=" + playerVec + "hit.point=" + hit.point);
                //if (weapon.Shot() == null) { return; }
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Shot() == null) { return; }
                //RPCを用いてプレイヤー間で発射タイミングと方向を同期する
                photonView.RPC(nameof(shot), RpcTarget.All, bulletVec, playernum, weaponflame.choiceWeapon);
                //photonView.RPC(nameof(shot), RpcTarget.All, 1 , playernum, weaponflame.choiceWeapon);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                weaponflame.weaponchange(-1);
                Debug.Log("down");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                weaponflame.weaponchange(1);
                Debug.Log("up");
            }
        }
    }
    //射撃同期の関数
    [PunRPC]
    private void shot(Vector3 target, int playernumber, int weaponID)
    {
        //弾の発射処理
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODOあんまうまくいってない気がする
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target, playernumber, weaponID);
    }
    [PunRPC]
    private void shot(int targetnumber, int playernumber, int weaponID)
    {
        //弾の発射処理
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODOあんまうまくいってない気がする
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(new Vector3 (5,5,5), playernumber, weaponID);
    }
}
