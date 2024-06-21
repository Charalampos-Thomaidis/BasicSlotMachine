using System.Collections;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private AudioManager audioManager;

    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
        audioManager = AudioManager.GetAudioManager();
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");

    }

    private IEnumerator Rotate()
    {
        audioManager.PlaySpinSound();
        rowStopped = false;
        timeInterval = 0.01f;

        for(int i = 0; i < 60; i++) 
        {
            if (transform.position.y <= -1.5f)
                transform.position = new Vector2(transform.position.x, 4.5f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }
        randomValue = Random.Range(60, 100);

        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -1.5f)
                transform.position = new Vector2(transform.position.x, 4.5f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.position.y == -1.5f)
            stoppedSlot = "melon";
        else if (transform.position.y == -0.75f)
            stoppedSlot = "horseshoe";
        else if (transform.position.y == 0f)
            stoppedSlot = "heart";
        else if (transform.position.y == 0.75f)
            stoppedSlot = "bell";
        else if (transform.position.y == 1.5f)
            stoppedSlot = "cherries";
        else if (transform.position.y == 2.25f)
            stoppedSlot = "clover";
        else if (transform.position.y == 3f)
            stoppedSlot = "lemon";
        else if (transform.position.y == 3.75f)
            stoppedSlot = "Lucky7_rainbow";
        else if (transform.position.y == 4.5f)
            stoppedSlot = "melon";

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}