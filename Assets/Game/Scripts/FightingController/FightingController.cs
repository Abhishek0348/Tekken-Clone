using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingController : MonoBehaviour
{
    [Header("Movement PLAYER")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    private Animator animator;
    private CharacterController characterController;

    [Header("Player Fight")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    public int attackDamage = 5;
    public string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    public float dodgeDistance = 2f;
    public Transform[] opponents;
    public float attackRange = 2.2f;

    [Header("Effect and Sound")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;

    public AudioClip[] hitSounds;

    [Header("Player Health")]
    public int maxHealth = 100;
    public int currentHealth;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            Debug.LogError("CharacterController component is missing.");

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator component is missing.");

        currentHealth = maxHealth;
    }

    private void Update()
    {
        PerformMovement();
        PerformDodgeFront();

        // Different keys for each attack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed: Performing Attack 1");
            PerformAttack(0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed: Performing Attack 2");
            PerformAttack(1);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed: Performing Attack 3");
            PerformAttack(2);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T pressed: Performing Attack 4");
            PerformAttack(3);
        }
    }

    void PerformMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-verticalInput, 0f, horizontalInput);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    void PerformAttack(int attackIndex)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            if (attackIndex < attackAnimations.Length)
            {
                animator.Play(attackAnimations[attackIndex]);
                int damage = attackDamage;
                Debug.Log("Player attacked with " + attackDamage + " damage using " + attackAnimations[attackIndex]);
                lastAttackTime = Time.time;
            }
            
            foreach (Transform opponent in opponents)
            {
                if(Vector3.Distance(transform.position, opponent.position) <= attackRange)
                {
                    Debug.Log("Opponent is in range");
                    // Play HIT / DAMAGE animation
                    opponent.GetComponent<OpponentAI>().StartCoroutine(opponent.GetComponent<OpponentAI>().PlayHitDamage(attackDamage));
                }
                else
                {
                    Debug.Log("Opponent is out of range");
                }
            }
        }
        else
        {
            Debug.Log("Attack is on cooldown");
        }
    }

    public void PerformDodgeFront()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed: Performing Dodge Front");
            animator.Play("DodgeFrontAnimation");
            Vector3 dodgeDirection = transform.forward * dodgeDistance;
            characterController.Move(dodgeDirection);
        }
    }

    public IEnumerator PlayHitDamage(int takeDamage)
    {
        yield return new WaitForSeconds(0.5f);

        // Play HIT / DAMAGE sound
        if(hitSounds.Length > 0 && hitSounds!=null)
        {
            int randomSound = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[randomSound], transform.position);
        }

        //decrease health
        currentHealth -= takeDamage;
        if(currentHealth <= 0)
        {
            Die();
        }

        //play animation
        animator.Play("HitDamageAnimation");
    }

    void Die()
    {
        Debug.Log("Player died");
        
    }

    public void Attack1Effect()
    {
        attack1Effect.Play();
    }
    public void Attack2Effect()
    {
        attack2Effect.Play();
    }
    public void Attack3Effect()
    {
        attack3Effect.Play();
    }
    public void Attack4Effect()
    {
        attack4Effect.Play();
    }
}
