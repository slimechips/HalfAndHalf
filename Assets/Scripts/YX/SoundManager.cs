using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static GameObject PlaySound()
    {
        GameObject soundObject = new GameObject("Death");
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.PlayOneShot(GameAssets.i.EnemyDeathClip);
        return soundObject;
    }
    public static GameObject PlayCollideWithShieldSound()
    {
        GameObject soundObject = new GameObject("Collide");
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.PlayOneShot(GameAssets.i.CollideWithShield);
        return soundObject;
    }
    public static GameObject EnemyDamage()
    {
        GameObject soundObject = new GameObject("EnemyHit");
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.PlayOneShot(GameAssets.i.EnemyHit);
        return soundObject;
    }
    public static GameObject PlayerShoot()
    {
        GameObject soundObject = new GameObject("PlayerShoot");
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.PlayOneShot(GameAssets.i.PlayerShoot);
        return soundObject;
    }
    public static GameObject EnemyShoot()
    {
        GameObject soundObject = new GameObject("EnemyShoot");
        AudioSource audio = soundObject.AddComponent<AudioSource>();
        audio.PlayOneShot(GameAssets.i.EnemyShoot);
        return soundObject;
    }
}
