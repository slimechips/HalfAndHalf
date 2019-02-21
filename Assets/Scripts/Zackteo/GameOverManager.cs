using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerShip playerShip;       // Reference to the player's health.
    public float restartDelay = 5f;         // Time to wait before restarting the level


    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level


    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // If the player has run out of health...
        if (playerShip.health <= 0)
        {
            // ... tell the animator the game is over.
            anim.SetTrigger("GameOver");

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
<<<<<<< HEAD
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("SampleScene");
=======
                SceneManager.LoadScene(0);
>>>>>>> d56e4a722f92d3a1ff37b3eb87b21e793e01c311
            }
        }
    }
}