using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Cinemachine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class logintest : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject chinemacine;
    private void Start()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var player =PhotonNetwork.Instantiate("Player",this.transform.position, Quaternion.identity);
        var camera = Instantiate(chinemacine, this.transform);
        var CM = camera.GetComponent<CinemachineVirtualCamera>();
        CM.Follow = player.transform;
        CM.LookAt = player.transform;
    }
}
