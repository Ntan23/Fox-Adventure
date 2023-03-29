using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    #region FloatVariables
    [SerializeField] private float powerUpDuration;
    [SerializeField] private float powerUpSpeed;
    [SerializeField] private float powerUpJumpForce;
    private float intialSpeed;
    private float intialJumpForce;
    #endregion

    #region OtherVariables
    [SerializeField] private PlayerController playerController;
    private Animator animator;
    #endregion

    void Start()
    {
        intialSpeed = playerController.GetSpeed();
        intialJumpForce = playerController.GetJumpForce();

        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Feedback");

            StartCoroutine(PowerUpEffect(other.gameObject));
        }
    }

    IEnumerator PowerUpEffect(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = Color.red;
        playerController.ChangeSpeedAndJumpForce(powerUpSpeed, powerUpJumpForce);
        yield return new WaitForSeconds(powerUpDuration);
        obj.GetComponent<SpriteRenderer>().color = Color.white;
        playerController.ChangeSpeedAndJumpForce(intialSpeed, intialJumpForce);
        gameObject.SetActive(false);
    }
}
