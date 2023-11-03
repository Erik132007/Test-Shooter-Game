using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Move Detailes")]
    [SerializeField] private float speed = 5.0f;

    private float moveHorizontal;
    private Vector3 movement;

    [Header("Bullet Modules")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float chargingTime;

    private bool isCharging;

    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private int healthAmount;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !isCharging)
        {
            Shoot();
            StartCoroutine(CharginigTime());
        }
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException($"{this} ERROR - Damage can't be less zero");

        healthAmount -= damage;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = healthAmount / 100;
    }

    IEnumerator CharginigTime()
    {
        isCharging = true;
        yield return new WaitForSeconds(chargingTime);
        isCharging = false;
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.instance.GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }
    }

    void PlayerMove()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.position += movement * speed * Time.deltaTime;
    }
}