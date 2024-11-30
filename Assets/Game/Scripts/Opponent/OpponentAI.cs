using System.Collections;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [Header("Movement Opponent")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    public Animator animator;
    public CharacterController characterController;

    [Header("Opponent Fight")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    public int attackDamage = 5;
    public int attackCount = 0;//
    public int randomNumber;
    public float attackRange = 2.2f;
    public FightingController[] fightingControllers;
    public Transform[] players;
    public bool isTakingDamage = false;//
    public string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    public float dodgeDistance = 2f;

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
        createRandomNumber();
        currentHealth = maxHealth;

    }

    private void Update()
    {

        if(attackCount == randomNumber)
        {
            attackCount = 0;
            createRandomNumber();
        }


        
        for(int i=0; i<fightingControllers.Length; i++) 
        {
            if (players[i].gameObject.activeSelf && Vector3.Distance(transform.position, players[i].position) <= attackRange)
            {
                animator.SetBool("Walking", false);
                if (Time.time - lastAttackTime > attackCooldown)
                {
                    int randomAttack = Random.Range(0, attackAnimations.Length);

                    if (!isTakingDamage)
                    {
                        PerformAttack(randomAttack);
                    }

                    // Play HIT / DAMAGE animation

                    fightingControllers[i].StartCoroutine(fightingControllers[i].PlayHitDamage(attackDamage));
                }   
            }
            else
            {
                if (players[i].gameObject.activeSelf)
                {
                    Vector3 direction = (players[i].position - transform.position).normalized;  // calculate the position of the player
                    characterController.Move(direction * movementSpeed * Time.deltaTime);  // move the opponent towards the player
                    Quaternion targetRotation = Quaternion.LookRotation(direction);  // rotate the opponent towards the player
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // smooth the rotation

                    animator.SetBool("Walking", true);
                }
            }
            
        }
    }

    public void PerformDodgeFront()
    { 
            Debug.Log("Space pressed: Performing Dodge Front");
            animator.Play("DodgeFrontAnimation");
            Vector3 dodgeDirection = -transform.forward * dodgeDistance;
            characterController.SimpleMove(dodgeDirection);
    }

    void PerformAttack(int attackIndex)
    {
         animator.Play(attackAnimations[attackIndex]);
         int damage = attackDamage;
         Debug.Log("Player attacked with " + attackDamage + " damage using " + attackAnimations[attackIndex]);
         lastAttackTime = Time.time;

         // Loop Through all the players

    }
    

    void createRandomNumber()
    {
        randomNumber = Random.Range(1, 5);
    }

    public IEnumerator PlayHitDamage(int takeDamage)
    {
        yield return new WaitForSeconds(0.5f);

        // Play HIT / DAMAGE sound
        if (hitSounds.Length > 0 && hitSounds != null)
        {
            int randomSound = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[randomSound], transform.position);
        }

        //decrease health
        currentHealth -= takeDamage;
        if (currentHealth <= 0)
        {
            Die();
        }

        //play animation
        animator.Play("HitDamageAnimation");
    }

    void Die()
    {
        Debug.Log("Opponent died");

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
