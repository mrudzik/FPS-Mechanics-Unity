using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HoldToPickUp : MonoBehaviour
{
	[Tooltip("Should be main camera")]
	public Camera		camera;
	[Tooltip("The layers the items to pickup will be on")]
	public LayerMask	layerMask;
	[Tooltip("The root of the images (progress image and item text should be a child of this too")]
	public RectTransform 	pickUpImageRoot;
	public Image 			progressImage;
	public Text 			itemNameText;


	private Item 	_itemBeingInteracted;
	private float 	_currentPickupTimerElapsed;
	private float 	_interactTime = 2f;
	public float 	interactRange = 2f;
	
	
	private bool 	_completed;
	private void Update()
	{
		SelectItemBeingInteractedFromRay();
		
		if (ItemTargeted())
		{
			pickUpImageRoot.gameObject.SetActive(true);
			if (Input.GetKey("e"))
			{
				if (!_completed)
					IncrementPickTimeTryToComplete();
			}
			else
			{
				_currentPickupTimerElapsed = 0;
				_completed = false;
			}
			UpdatePickUpProgress();
		}
		else
		{
			pickUpImageRoot.gameObject.SetActive(false);
			_currentPickupTimerElapsed = 0;
			_completed = false;
		}
		
	}

	private void 	UpdatePickUpProgress()
	{
		float progress = _currentPickupTimerElapsed / _interactTime;

		progressImage.fillAmount = progress;
	}
	
	private void 	IncrementPickTimeTryToComplete()
	{
		_currentPickupTimerElapsed += Time.deltaTime;
		if (_currentPickupTimerElapsed >= _interactTime)
		{
			Interact();
			_completed = true;
		}
	}
	
	private void 	Interact()
	{
		if (_itemBeingInteracted != null)
		{
			_itemBeingInteracted.Action();
		}
		else
			Debug.Log("INTERACTION FAILED");
	}

	private bool 	ItemTargeted()
	{
		return _itemBeingInteracted != null;
	}
	
	private void 	SelectItemBeingInteractedFromRay()
	{
		Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
		Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

		RaycastHit hitInfo;
		// Will shoot a ray in a world and tries to find certain layer
		// in range of maxDistance
		if (Physics.Raycast(ray, out hitInfo, interactRange, layerMask))
		{
			// Check to find a needed item
			var hitItem = hitInfo.collider.GetComponent<Item>();
			
			if (hitItem == null)
			{	// If found something but that is not an Item
				_itemBeingInteracted = null;
			}
			else if (hitItem != null && hitItem != _itemBeingInteracted)
			{	// If found Item that is not BeingInteracted
				_itemBeingInteracted = hitItem;
				itemNameText.text = "Interact with " + _itemBeingInteracted.gameObject.name;
				_currentPickupTimerElapsed = 0;
				_interactTime = _itemBeingInteracted.InteractionTime;
			}
		}
		else
		{	// If found nothing
			_itemBeingInteracted = null;
		}

	}
}
