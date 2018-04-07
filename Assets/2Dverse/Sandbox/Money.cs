using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

	public Text Coins;

	public int coins { get; private set; }

	private void OnEnable () {
		GameState.onLoad += UpdateCoins;
	}

	private void OnDisable () {
		GameState.onLoad -= UpdateCoins;
	}

	public void UpdateCoins () {
		coins = GameState.GetInt("coin");
		GameState.SetInt("coin", 1000);
		Coins.text = coins.ToString();
	}

	public void Credit (int i, Action<bool> done = null) {
		if (i <= 0) {
			if (done != null) done(false);
			return;
		}

		coins += i;

		GameState.SetInt("coin", coins);
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
		Coins.text = coins.ToString();
		done(true);
	}

}