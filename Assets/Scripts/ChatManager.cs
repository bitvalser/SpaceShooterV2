using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour, IChatClientListener {

	private const string GeneralChannel = "general";
	private const string AppId = "1f5963e2-3bbe-4985-9b63-e52b6c347174";
	private const string AppVersion = "1.0";
	private ChatClient _chat;
	private string _name = "";
	private string _chatText = "";
	private string _privateText = "";
	private string _input = "";
	private bool _connected;
	void Start()
	{
		DontDestroyOnLoad(gameObject);
		Application.runInBackground = true;

		_chat = new ChatClient(this);
	}
	void Update()
	{
		if (_chat != null)
			_chat.Service();
	}
	void OnGUI()
	{
		if (!_connected)
		{
			_name = GUI.TextField(new Rect(10, 10, 200, 20), _name);
			if (GUI.Button(new Rect(10, 35, 80, 20), "Enter"))
			{
				if (!string.IsNullOrEmpty(_name) && _name.Length > 0)
					Connect();
			}
		}
		else
		{
			GUI.TextArea(new Rect(10, 10, 200, 200), _chatText);
			_input = GUI.TextField(new Rect(10, 215, 100, 20), _input);
			if (GUI.Button(new Rect(115, 215, 80, 20), "Send"))
			{
				if (!string.IsNullOrEmpty(_input) && _input.Length > 0)
				{
					SendMessage(_input);
					_input = "";
				}
			}
		}

	}
	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			UnityEngine.Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		else
		{
			UnityEngine.Debug.Log(message);
		}
	}
	void OnApplicationQuit()
	{
		if (_chat != null)
			_chat.Disconnect();
	}

	private void Connect()
	{
		_chat.Connect (AppId, AppVersion, new ExitGames.Client.Photon.Chat.AuthenticationValues(_name));
	}

	private void SendMessage(string message)
	{
		if (message.StartsWith("/"))
		{
			ParseCommand(message);
			return;
		}

		var mas = message.Split(new[] {':'});
		if (mas.Length == 2)
		{
			_chat.SendPrivateMessage(mas[0], mas[1]);
			return;
		}

		_chat.PublishMessage(GeneralChannel, message);
	}

	private void ParseCommand(string command)
	{
		switch (command.Remove(0,1))
		{
		case "clear":
			{
				_chatText = "";
				_chat.PublicChannels[GeneralChannel].ClearMessages();
				break;
			}
		}
	}

	public void OnDisconnected()
	{
		_connected = false;
	}

	public void OnConnected()
	{
		_connected = true;

		_chat.Subscribe(new[] {GeneralChannel}, -1);


		_chat.SetOnlineStatus(ChatUserStatus.Online);
	}

	public void OnChatStateChange(ChatState state)
	{

	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		if (channelName == GeneralChannel)
		{
			for (int i = 0; i < senders.Length; i++)
			{
				_chatText = senders[i] + ":" + messages[i] + "\r\n" + _chatText;
			}
		}
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		Debug.Log("Private message! " + sender + ":" + message);
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		foreach (var channel in channels)
		{
			_chat.PublishMessage(channel, "вошел.");
		}
	}

	public void OnUnsubscribed(string[] channels)
	{

	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.Log(string.Format("Friend {0} set status to {1}", user, status));
	}
}
