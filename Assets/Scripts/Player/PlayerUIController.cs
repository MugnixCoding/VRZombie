using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Transform HeadSet;
    [SerializeField] private Image overlay;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float fadeSpeed = 1f;

    private float damageDurationTimer;
    private bool isDamageCoroutineExecuting;

    private Vector3 direction;
    void Start()
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        isDamageCoroutineExecuting = false;
        Player player = null;
        if (TryGetComponent<Player>(out player))
        {
            player.OnTakeDamage += TakeDamage;
            player.OnDead += Death;
        }
    }
    #region health hurt dead
    private void TakeDamage(object sender, DamageEventArgs e)
    {
        damageDurationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.5f);

        direction = e.DirectionDamgeFrom.normalized;
        float angle = Vector3.SignedAngle(direction, HeadSet.forward,Vector3.up);
        overlay.transform.localEulerAngles = new Vector3(0,0,angle);

        if (!isDamageCoroutineExecuting)
        {
            StartCoroutine(DamageOverlayFade());
        }
    }
    private void Death(object sender, EventArgs e)
    {
        damageDurationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.5f);
    }
    private IEnumerator DamageOverlayFade()
    {
        isDamageCoroutineExecuting = true;
        while (overlay.color.a > 0)
        {
            damageDurationTimer += Time.deltaTime;
            if (damageDurationTimer >= duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
            yield return new WaitForFixedUpdate();
        }
        isDamageCoroutineExecuting = false;
    }
    #endregion
}
