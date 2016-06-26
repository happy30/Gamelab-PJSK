using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public int healthBonus;
    public int attackPowerBonus;
    public int weaponID;

    public StatsManager stats;
    public PlayerController player;
    public UIManager ui;
    public AudioClip swingSound;

    //Once this weapon is activated, add stats permanently (WIP)
    void Start()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        stats.maxHealth += healthBonus;
        stats.attackPower += attackPowerBonus;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButtonDown("Fire1") && !player.inConversation)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
	}

    public void AttackMonster(int attackPower)
    {
        if(player.monster != null)
        {
            player.monster.GetComponent<EnemyBehaviour>().health -= attackPower;
            ui.UISound.PlayOneShot(ui.weaponHit, 1);
        }
    }

    public void SwingWeapon()
    {
        ui.UISound.PlayOneShot(swingSound, 1);
    }
}
