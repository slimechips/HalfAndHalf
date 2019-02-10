﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Ship : MonoBehaviour, ICollidesWithProjectiles {

    [SerializeField] private float _maxhealth = 100;
    public float maxHealth {
        get;
    }
    private float _health = 100;
    public float health {
        get;
    } // To change this, call the Damage or Heal functions

    public new Rigidbody2D rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        //if (rigidbody == null) {
        //    Debug.LogWarning(gameObject.name + " does not have a rigidbody, self-deleting");
        //    Destroy(this);
        //}
    }

    public float Damage(float amt) {
        float prevhealth = _health;
        _health -= amt;
        if (_health <= 0) {
            _health = 0;
            OnDeath();
        }
        return prevhealth - _health;
    }

    public float Heal(float amt) {
        float prevhealth = _health;
        _health += amt;
        if (_health >= _maxhealth) {
            _health = _maxhealth;
        }
        return _health - prevhealth;
    }

    public abstract void OnDeath(); // child classes must implement
    public abstract bool ReceiveProjectile(Projectile p);
}
