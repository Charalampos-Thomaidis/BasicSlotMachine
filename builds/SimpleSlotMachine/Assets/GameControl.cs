using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int scoreValue;

    private bool resultsChecked = false;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetAudioManager();
    }

    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            scoreValue = 0;
            scoreText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            scoreText.enabled = true;
            scoreText.text = "Score: " + scoreValue;
        }
    }
    private void OnMouseDown()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            StartCoroutine("PullHandle");
    }

    private IEnumerator PullHandle()
    {
        audioManager.PlayClickSound();

        HandlePulled();

        yield return new WaitForSeconds(0.1f);
    }

    private void CheckResults()
    {
        switch (rows[0].stoppedSlot)
        {
            case "bell":
                if (rows[1].stoppedSlot == "bell" && rows[2].stoppedSlot == "bell")
                    scoreValue = 100;
                else if (rows[1].stoppedSlot == "bell" || rows[2].stoppedSlot == "bell")
                    scoreValue = 50;
                break;
            case "lemon":
                if (rows[1].stoppedSlot == "lemon" && rows[2].stoppedSlot == "lemon")
                    scoreValue = 200;
                else if (rows[1].stoppedSlot == "lemon" || rows[2].stoppedSlot == "lemon")
                    scoreValue = 150;
                break;
            case "cherries":
                if (rows[1].stoppedSlot == "cherries" && rows[2].stoppedSlot == "cherries")
                    scoreValue = 300;
                else if (rows[1].stoppedSlot == "cherries" || rows[2].stoppedSlot == "cherries")
                    scoreValue = 250;
                break;
            case "melon":
                if (rows[1].stoppedSlot == "melon" && rows[2].stoppedSlot == "melon")
                    scoreValue = 400;
                else if (rows[1].stoppedSlot == "melon" || rows[2].stoppedSlot == "melon")
                    scoreValue = 350;
                break;
            case "heart":
                if (rows[1].stoppedSlot == "heart" && rows[2].stoppedSlot == "heart")
                    scoreValue = 500;
                else if (rows[1].stoppedSlot == "heart" || rows[2].stoppedSlot == "heart")
                    scoreValue = 450;
                break;
            case "clover":
                if (rows[1].stoppedSlot == "clover" && rows[2].stoppedSlot == "clover")
                    scoreValue = 600;
                else if (rows[1].stoppedSlot == "clover" || rows[2].stoppedSlot == "clover")
                    scoreValue = 550;
                break;
            case "horseshoe":
                if (rows[1].stoppedSlot == "horseshoe" && rows[2].stoppedSlot == "horseshoe")
                    scoreValue = 700;
                else if (rows[1].stoppedSlot == "horseshoe" || rows[2].stoppedSlot == "horseshoe")
                    scoreValue = 650;
                break;
            case "Lucky7_rainbow":
                if (rows[1].stoppedSlot == "Lucky7_rainbow" && rows[2].stoppedSlot == "Lucky7_rainbow")
                    scoreValue = 7777;
                else if (rows[1].stoppedSlot == "Lucky7_rainbow" || rows[2].stoppedSlot == "Lucky7_rainbow")
                    scoreValue = 777;
                break;
            default:
                scoreValue = 0;
                break;
        }

        if (rows[1].stoppedSlot == rows[2].stoppedSlot)
        {
            switch (rows[1].stoppedSlot)
            {
                case "bell":
                    scoreValue = 50;
                    break;
                case "lemon":
                    scoreValue = 150;
                    break;
                case "cherries":
                    scoreValue = 250;
                    break;
                case "melon":
                    scoreValue = 350;
                    break;
                case "heart":
                    scoreValue = 450;
                    break;
                case "clover":
                    scoreValue = 550;
                    break;
                case "horseshoe":
                    scoreValue = 650;
                    break;
                case "Lucky7_rainbow":
                    scoreValue = 777;
                    break;
                default:
                    scoreValue = 0;
                    break;
            }
        }

        if (scoreValue > 0)
        {
            audioManager.PlayWinSound();
        }

        resultsChecked = true;

        audioManager.StopSpinSound();
    }
}