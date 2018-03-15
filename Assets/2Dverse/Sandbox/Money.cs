using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

	public Text Coins;

	public int coins { get; private set; }

	public void UpdateCoins () {
		coins = GameState.GetInt("coin");
		Coins.text = coins.ToString();
	}

	public void Credit (int i, Action<bool> done = null) {
		if (i <= 0) {
			if (done != null) done(false);
			return;
		}

		coins += i;

		GameState.SetInt("coin", coins);
//		CloudSave.SetInt("coin", coins);
		Coins.text = coins.ToString();
		if (done != null) done(true);
	}

	public void Debit (int i, Action<bool> done) {
		if (i <= 0 || coins - i < 0) {
			done(false);
			return;
		}

		coins -= i;

		GameState.SetInt("coin", coins);
//		CloudSave.SetInt("coin", coins);
		Coins.text = coins.ToString();
		done(true);
	}

}