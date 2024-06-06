using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    AudioManager audioManager;
    public float lifespan = 0.5f;
    protected PlayerStats target;
    protected float speed;
    Vector2 initialPosition;

    [System.Serializable]
    // bobbing animation to make the exp gems more visible
    public struct BobbingAnimation
    {
        public float frequency;
        public Vector2 direction;

    }
    public BobbingAnimation bobbingAnimation = new BobbingAnimation { frequency = 2f, direction = new Vector2(0, 0.3f) };

    [Header("Bonuses")]
    public int experience;

    public void Start()
    {
        initialPosition = transform.position;
    }

    private void Awake()
    {
        // Find and assign the AudioManager component
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected virtual void Update()
    {
        if (target)
        {
            Vector2 distance = target.transform.position - transform.position;
            if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
            {
                transform.position += (Vector3)distance.normalized * speed * Time.deltaTime;
            }
            else
                Destroy(gameObject);

        }
        else
        {
            // adding bobbing animation to the pickup
            transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin(Time.time * bobbingAnimation.frequency);
        }
    }

    public virtual bool Collect(PlayerStats target, float speed, float lifespan = 0f)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            if (lifespan > 0) this.lifespan = lifespan;
            Destroy(gameObject, Mathf.Max(0.01f, this.lifespan));
            // experience taken sound effect
            audioManager.PlaySFX(audioManager.expTaken);
            return true;
        }
        return false;
    }

    protected virtual void OnDestroy()
    {
        if (!target) return;
        if (experience != 0) target.IncreaseExperience(experience); // increase the player's experience if the destroyed target is exp ("this game dont have any other pickups rn other than exp")
    }
}
