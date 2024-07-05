using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour, IDmg
{
    [SerializeField] int maxHP, currentHP;
    [SerializeField] int playerLifes = 3;
    [SerializeField] Text hpText;
    [SerializeField] Image hpSlider;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    void Start()
    {
        currentHP = maxHP;
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / (float)maxHP;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "SpikesTrap")
        {
            Damage damage = coll.gameObject.GetComponent<Damage>();
            currentHP -= damage.DamageValue;
            hpText.text = currentHP + "/" + maxHP;
            hpSlider.fillAmount = (float)currentHP / (float)maxHP;
        }
        if (currentHP <= 0)
        {
            isAlive = false;
            animator.SetBool("alive", false);
        }

        if (coll.gameObject.layer == 8)
        {
            Healing(25);
        }
        if (coll.gameObject.layer == 9)
        {
            Healing(50);
        }
    }
    public void ReciveDmg(int dmgValue)
    {
        if (playerMovement.IsBlocking == false)
        {
            currentHP -= dmgValue;
        }
        if (currentHP <= 0)
        {
            if (playerLifes == 0)
            {
                isAlive = false;
                currentHP = 0;
                IsDead();
            }
            else
            {
                playerLifes --;
                currentHP = maxHP;
            }
        }
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / (float)maxHP;
    }

    public void Healing(int healamount)
    {
        currentHP += healamount;
        hpText.text = currentHP + "/" + maxHP;
        hpSlider.fillAmount = (float)currentHP / (float)maxHP;
    }

    public void IsDead()
    {
        animator.SetBool("alive", false);
        hpSlider.gameObject.SetActive(false);
    }
}
