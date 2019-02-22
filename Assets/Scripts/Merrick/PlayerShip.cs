using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : Ship
{

    public static PlayerShip playerShip = null;

    // reference to sprites
    public Sprite[] playerSprites;
    public Image healthIm;
    private SpriteRenderer sprite;
    public Slider energySlider;                                 // Reference to the UI's energy bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 4.5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.



    private float _energy = 100;
    public float energy {
        get { return _energy; }
        set { _energy = Mathf.Clamp(value, 0, 100); }
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (playerShip != null) Destroy(playerShip);
        playerShip = this;
    }

    private void Update()
    {
        // alternate between the different images for thrusters
        if (Input.GetAxis("p1 b") > 0.1f && Input.GetAxis("p2 b") == 0)
        {
            sprite.sprite = playerSprites[2];
        }
        else if (Input.GetAxis("p1 b") == 0 && Input.GetAxis("p2 b") > 0.1f)
        {
            sprite.sprite = playerSprites[1];
        }
        else if (Input.GetAxis("p1 b") > 0.1f && Input.GetAxis("p2 b") > 0.1f)
        {
            sprite.sprite = playerSprites[3];
        }
        else
        {
            sprite.sprite = playerSprites[0];
        }

        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
            playerAudio.Play();
        }
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        // visualise health using the spaceship
        healthIm.fillAmount = 1f - health / maxHealth;


        energySlider.value = energy;
    }

    public void DamageFlash()
    {
        damaged = true;
    }

    public override void OnDeath()
    {
        // spawn explosions
        // trigger defeat screen
        GameObject go = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
        go.transform.position = transform.position;
        go.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
        
        // Tell the animator that the player is dead.
        isDead = true;
        anim.SetTrigger("Die");
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        if (sprite != null) sprite.enabled = false;
    }

    public override bool ReceiveProjectile(Projectile p)
    {
        if (!p.isPlayerProjectile)
        {
            damaged = true;
            Damage(p.damage);
            Damage(p.damagePerSecond * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
