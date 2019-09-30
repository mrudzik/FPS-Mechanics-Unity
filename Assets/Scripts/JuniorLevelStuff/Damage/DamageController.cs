using UnityEngine;


public class DamageController : MonoBehaviour
{
	public int 		rangeHp1, rangeHp2;
	protected int	maxHp;
	
	[HideInInspector]
	public int 		currentHp;
	public Death 	_deathScript;

	public AudioSource 	soundSource;
	public AudioClip 	hurt;
	public AudioClip 	death;
	
	private void Start()
	{
		maxHp = Random.Range(rangeHp1, rangeHp2 + 1);
		currentHp = maxHp;
		Visualize();
	}

	private void Update()
	{
		Visualize();
	}

	
	
	protected virtual void 	Visualize(){}
	
	
	public virtual void     TakeDamage(int damage)
	{
		if (currentHp > 0)
			currentHp -= damage;
		else
			return;
		
		if (currentHp <= 0)
		{
			_deathScript.Die();
			if (soundSource != null)
			{
				soundSource.clip = death;
				soundSource.Play();
			}
			return;
		}
		if (soundSource != null)
		{
			soundSource.clip = hurt;
			soundSource.Play();
		}
		


	}

	
}
