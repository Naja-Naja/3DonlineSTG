using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Cinemachine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class logintest : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject chinemacine;
    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var player =PhotonNetwork.Instantiate("Player",this.transform.position, Quaternion.identity);
        var camera = Instantiate(chinemacine, this.transform);
        var CM = camera.GetComponent<CinemachineVirtualCamera>();
        CM.Follow = player.transform;
        CM.LookAt = player.transform;
    }
}
