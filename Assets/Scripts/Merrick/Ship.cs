using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour, ICollidesWithProjectiles, ICollideWithWalls {

    [SerializeField] private float _maxhealth = 100;
    [SerializeField] private float _wallCollideDistance = 5;

    public float maxHealth {
        get { return _maxhealth; }
    }
    private float _health = 100;
    public float health {
        get { return _health; }
        set { _health = value; }
    } // To change this, call the Damage or Heal functions

    [System.NonSerialized] public new Rigidbody2D rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        //if (rigidbody == null) {
        //    Debug.LogWarning(gameObject.name + " does not have a rigidbody, self-deleting");
        //    Destroy(this);
        //}
    }

    public void BaseInitialise(float maxHealth)
    {
        this._maxhealth = maxHealth;
        this.health = maxHealth;
    }

    public float Damage(float amt) {
        float prevhealth = health;
        health -= amt;
        if (health <= 0) {
            health = 0;
            OnDeath();
        }
        return prevhealth - health;
    }

    public float Heal(float amt) {
        float prevhealth = health;
        health += amt;
        if (health >= _maxhealth) {
            health = _maxhealth;
        }
        return health - prevhealth;
    }

    protected virtual void Update()
    {

    }

    public abstract void Shoot();
    public abstract void OnDeath(); // child classes must implement
    public abstract bool ReceiveProjectile(Projectile p);

    public void CollideWithWall(Wall wall)
    {
        RestoreLocation();
    }

    public void RestoreLocation()
    {
        Vector3 restoreTranslate = -this.transform.up * _wallCollideDistance;
        Vector3 restoreLoc = this.transform.position + restoreTranslate;
        Debug.Log(restoreLoc);
        if (EnemyManager.current.WithinBounds(restoreLoc) == ValidCoord.X_LARGE)
        {
            restoreLoc.x = 2 * EnemyManager.current.WorldXMax - restoreLoc.x;
            Debug.Log(restoreLoc);

        }

        if (EnemyManager.current.WithinBounds(restoreLoc) == ValidCoord.X_SMALL)
        {
            restoreLoc.x = 2 * EnemyManager.current.WorldXMin - restoreLoc.x;
            Debug.Log(restoreLoc);

        }

        if (EnemyManager.current.WithinBounds(restoreLoc) == ValidCoord.Y_LARGE)
        {
            restoreLoc.y = 2 * EnemyManager.current.WorldYMax - restoreLoc.y;
            Debug.Log(restoreLoc);

        }

        if (EnemyManager.current.WithinBounds(restoreLoc) == ValidCoord.Y_SMALL)
        {
            restoreLoc.y = 2 * EnemyManager.current.WorldYMin - restoreLoc.y;
            Debug.Log(restoreLoc);

        }

        if (EnemyManager.current.WithinBounds(restoreLoc) != ValidCoord.VALID)
        {
            restoreLoc = new Vector3(0, 0, 0);
            Debug.Log(restoreLoc);

        }

        this.transform.position = restoreLoc;
    }
}
