using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private readonly float gamePlayingTimerMax = 10f;
    private State state;
    private float waitingToStartTimer = 1f;

    public static KitchenGameManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f) {
                    state = State.CoundownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.CoundownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GameOver:
                break;
        }
    }

    public event EventHandler OnStateChanged;

    public bool isGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool isCountdownToStartActive() {
        return state == State.CoundownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }

    private enum State {
        WaitingToStart,
        CoundownToStart,
        GamePlaying,
        GameOver
    }
}