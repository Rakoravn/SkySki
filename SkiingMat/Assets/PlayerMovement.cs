using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody rb;
    public float mSpeed;
    public float constantStillSpeed;
    Vector2 moveDir = Vector2.zero;
    float lastMoveX = 0f;
    float time = 0;
    float timeMinus = 0;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highScoreText;
    float highscore = 0;
    public Transform ellenSpine;
    public Transform ellenLeftLeg;
    public Transform ellenRightLeg;
    enum State { PRE, STARTED, FINISHED };
    State currentState = State.PRE;
    void Start() {
        rb.AddForce(new Vector3(0f, 0f, 0.5f), ForceMode.Impulse);
        SetTimeText(time);
        if (PlayerPrefs.HasKey("HIGHSCORE"))
        {
            string s = PlayerPrefs.GetString("HIGHSCORE");
            SetHighscoreText(s);
            float.TryParse(s.Replace(".", ","), out highscore);
        }
        else
        {
            SetHighscoreText("0");
            PlayerPrefs.SetString("HIGHSCORE", "0");
        }
        currentState = State.STARTED;
    }

    // Update is called once per frame
    void Update() {
        switch (currentState)
        {
            case State.PRE:
                break;
            case State.STARTED:
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");
                moveDir = new Vector2(moveX, moveY);
                time += Time.deltaTime;
                SetTimeText(GetTotalTime());
                break;
            case State.FINISHED:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate() {
        //rb.velocity = new Vector3(moveDir.x * mSpeed, 0, constantStillSpeed);
        float actualMoveDir = Mathf.Lerp(lastMoveX, moveDir.x, 0.03f);
        float turnPercentage;
        if (actualMoveDir > 0) {
            turnPercentage = actualMoveDir;
        } else if (actualMoveDir == 0) {
            turnPercentage = 0;
        } else {
            turnPercentage = actualMoveDir - (actualMoveDir*2);
        }
        transform.rotation = Quaternion.Euler(9.5f, actualMoveDir * 55f, -actualMoveDir * 2f);
        ellenSpine.rotation = Quaternion.Euler(ellenSpine.eulerAngles.x, ellenSpine.eulerAngles.y, (-actualMoveDir * 18f) - 90);
        ellenRightLeg.rotation = Quaternion.Euler(ellenRightLeg.eulerAngles.x, ellenRightLeg.eulerAngles.y, (actualMoveDir * 10f) - 90);
        ellenLeftLeg.rotation = Quaternion.Euler(ellenLeftLeg.eulerAngles.x, ellenLeftLeg.eulerAngles.y, (-actualMoveDir * 10f) + 90);
        rb.AddForce(new Vector3(actualMoveDir * mSpeed, 0, constantStillSpeed * (1.35f - turnPercentage)));
        lastMoveX = actualMoveDir;
        //Debug.Log(actualMoveDir * 20f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Killzone")
        {
            SceneManager.LoadScene(0);
        }
        else if (collision.gameObject.tag == "Boost")
        {
            rb.AddForce(new Vector3(0f, 0f, 5f), ForceMode.Impulse);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            currentState = State.FINISHED;
            Debug.Log(highscore);
            if (GetTotalTime() < highscore || highscore == 0)
            {
                PlayerPrefs.SetString("HIGHSCORE", GetTotalTime().ToString("F2").Replace(",", "."));
            }
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp1")
        {
            timeMinus += 1;
            StartCoroutine(changeTimeTextColor());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator changeTimeTextColor()
    {
        timerText.color = Color.green;
        yield return new WaitForSeconds(0.4f);
        timerText.color = Color.white;
    }

    public void SetTimeText(float newTime)
    {
        timerText.text = "Time: "+newTime.ToString("F2").Replace(",", ".");
    }

    public void SetHighscoreText(string hs)
    {
        highScoreText.text = "Best Time: " + hs;
    }

    public float  GetTotalTime()
    {
        return time - timeMinus;
    }
}
