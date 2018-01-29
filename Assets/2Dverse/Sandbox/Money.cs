using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Money : MonoBehaviour {

	public Text Coins;

	public int coins { get; private set; }

	private void OnEnable () {
		CloudSave.GetInt("coin", coin => {
			coins = coin;
			Coins.text = coins.ToString();
		});
	}

	public void Credit (int i, Action<bool> done = null) {
		if (i == 0) {
			if (done != null) done(false);
			return;
		}

		coins += i;

		CloudSave.SetInt("coin", coins, res => {
			Coins.text = coins.ToString();
			if (done != null) done(true);
		});
	}

	public void Debit (int i, Action<bool> done) {
		if (i == 0) {
			done(false);
			return;
		}

		if (coins - i < 0) {
			done(false);
			return;
		}

		coins -= i;
		CloudSave.SetInt("coin", coins, res => {
			Coins.text = coins.ToString();
			done(true);
		});
	}

}