using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    private bool isTouched = false;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isTouched = false;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isTouched)
        {
            isTouched = true;
            moveSpeed = -moveSpeed;
            Debug.Log(moveSpeed);
            FlipEnemy();
        }
    }

    private void FlipEnemy()
    {
        Debug.Log("flip");
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
