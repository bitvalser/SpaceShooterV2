using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pvpcontorl : Photon.MonoBehaviour {
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("v1");
	}
	void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom ("PVP", new RoomOptions (), TypedLobby.Default);
	}
	void OnJoinedRoom(){

		if (PhotonNetwork.countOfPlayersInRooms < 1) {
			Vector3 spawn = new Vector3 (212, 178, 1983);
			PhotonNetwork.Instantiate ("fighter03 1", spawn, Quaternion.Euler (0, 0, 0), 0);

		} 
		else {
			Vector3 spawn = new Vector3 (68, 178, 1589);
			PhotonNetwork.Instantiate ("fighter02", spawn, Quaternion.Euler (0, 180, 0), 0);
		}
	}
	[PunRPC]
	void KILL(GameObject other)
	{
		Destroy(other);
	}

}