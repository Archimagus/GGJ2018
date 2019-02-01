using UnityEngine;
using UnityEngine.UI;

public class InventoryImage : MonoBehaviour
{
	public static InventoryImage Instance { get; private set; }
	[SerializeField]private Image _inventoryImage = null;
	private Animator _animator;


	void Awake()
	{
		Instance = this;
		_animator = GetComponent<Animator>();
		_inventoryImage.enabled = false;
	}
	public void SetSprite(Sprite sprite)
	{
		_inventoryImage.sprite = sprite;
		if (sprite != null)
			Flash();
		else
			_inventoryImage.enabled = false;

	}

	public void Flash()
	{
		_inventoryImage.enabled = true;
		_animator.SetTrigger("Flash");
	}
}
