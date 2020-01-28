using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    private GameObject _player;
    private float margin = 5f;

    public GameObject player
    {
        get { return _player; }
    }

    private GameObject _em;

    private float _rotSpd, _moveSpd, _shotSpd;

    public int reward;
    public float rotSpd
    {
        get { return _rotSpd; }
    }
    public float moveSpd
    {
        get { return _moveSpd; }
    }
    public float shotSpd
    {
        get { return _shotSpd; }
    }

    private float _shootRange, _shootDelay;
    public float shootRange
    {
        get { return _shootRange; }
    }
    public float shootDelay
    {
        get { return _shootDelay; }
    }
    private bool _canShoot = false;
    public bool canShoot
    {
        get { return _canShoot; }
    }

    private float _moveRange;
    public float moveRange
    {
        get { return _moveRange; }
    }

    public void Initialise(GameObject player, GameObject em, params float[] list)
    {
        BaseInitialise(list[0]);
        this._player = player;
        this._rotSpd = list[1];
        this._moveSpd = list[2];
        this._shotSpd = list[3];
        this._shootRange = list[4];
        this._shootDelay = list[5];
        this._moveRange = list[6];
        this._em = em;
        StartCoroutine(DelayShot());
    }

    public void BasicRotate()
    {
        // rotate the transform to face the player
        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpd);
    }

    public void BasicMovement()
    {
        // move towards the player
        //transform.position += Vector3.Normalize(targetDir) * Time.deltaTime * moveSpd;
        transform.position += transform.up * Time.deltaTime * moveSpd;
    }

    private IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(_shootDelay);
        _canShoot = true;
    }

    public override void OnDeath()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/explosionAnim") as GameObject);
        go.transform.position = transform.position;
        Destroy(gameObject);
        GameObject sound = SoundManager.PlaySound();
        Destroy(sound, 3f);
        EnemyManager.current.UpdateCurEnemies(this);
        player.GetComponent<PlayerShip>().addScore(reward);

    }

    public override bool ReceiveProjectile(Projectile p)
    {
        if (p.isPlayerProjectile)
        {
            Damage(p.damage);
            Damage(p.damagePerSecond * Time.fixedDeltaTime);
            GameObject sound = SoundManager.EnemyDamage();
            Destroy(sound, 3f);
            return true;
        }
        return false;
    }

    public override void Shoot()
    {
        // spawn in a bullet projectile that travels in the current direction that the transform is facing
        GameObject bullet = Instantiate(Resources.Load("Prefabs/EBullet", typeof(GameObject))) as GameObject;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Bullet>().Initialise(_shotSpd);
        _canShoot = false;
        StartCoroutine(DelayShot());
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 finPos = screenPos;
        if (screenPos.x < -margin || screenPos.x > Camera.main.pixelWidth + margin)
        {
            if (screenPos.x < -margin) finPos = Vector3.right * (Camera.main.pixelWidth + margin / 2) + screenPos;
            else finPos = Vector3.left * (Camera.main.pixelWidth + margin / 2) + screenPos;
        }
        else if (screenPos.y < -margin || screenPos.y > Camera.main.pixelHeight + margin)
        {
            if (screenPos.y < -margin) finPos = Vector3.up * (Camera.main.pixelHeight + margin / 2) + screenPos;
            else finPos = Vector3.down * (Camera.main.pixelHeight + margin / 2) + screenPos;
        }
        transform.position = Camera.main.ScreenToWorldPoint(finPos);

    }
}
