using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public PlayerMovementController moveScript;
    private CharacterController myChar;
    private TrailRenderer trail;
    private Gradient _trailColor;


    [Header("Player Dash")]
    public float dashSpeed;
    public float dashTime;
    public Transform playerBody;
    public bool isDashing;
    public Gradient dashColor;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        myChar = gameObject.GetComponent<CharacterController>();
            moveScript = GetComponent<PlayerMovementController>();
    }

    //Dash
    private void OnDash(InputValue Dash)
    {
        if (Dash.isPressed)
        {
            if (isDashing == true)
            {
                return;
            }

            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Dash/Dash");
            StartCoroutine(QuickMove()); //calling on IEnumerator
        }
    }

    //What the Dash does
    IEnumerator QuickMove()
    {
        _trailColor = trail.colorGradient;
        trail.colorGradient = dashColor;
        isDashing = true;
        float timer = 0;

        while (timer < dashTime)
        {
            timer += Time.deltaTime;

            var t = timer / dashTime;

            myChar.Move(Vector3.Lerp(Vector3.zero , new Vector3(moveScript.leftStickPosition.x, 0, moveScript.leftStickPosition.y) * dashSpeed / 100, t));
            yield return new WaitForEndOfFrame();
        }

        isDashing = false;
        trail.colorGradient = _trailColor;

    }
}
