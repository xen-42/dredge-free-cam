using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DredgeFreeCam
{
	public class FreeCam : MonoBehaviour
	{
		public static FreeCam Instance { get; private set; }

		public bool IsActive { get; private set; }
		public Camera oldCamera;
		public Camera camera;

		public float speed = 5f;
		public float lookSpeed = 45f;

		private float _yRot;
		private float _xRot;

		private bool _couldMove;

		private GameObject _ui;

		public void Start()
		{
			if (Instance != null)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
				oldCamera = Camera.current;

				oldCamera = Camera.current;
				camera = gameObject.AddComponent<Camera>();
				camera.enabled = false;

				_ui = GameObject.Find("GameCanvases");
			}
		}

		private void Toggle()
		{
			IsActive = !IsActive;

			Plugin.Log($"Freecam active: {IsActive}");

			// Track old values if now active
			if (IsActive)
			{
				oldCamera = Camera.current;
				_couldMove = GameManager.Instance.Player.Controller.IsMovementAllowed;
			}

			GameManager.Instance.Player.Controller.IsMovementAllowed = IsActive ? false : _couldMove;
			camera.enabled = IsActive;
			oldCamera.enabled = !IsActive;
			_ui.SetActive(!IsActive);
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.O))
			{
				Toggle();
			}

			if (IsActive)
			{
				var dir = Vector3.zero;
				if (Input.GetKey(KeyCode.W))
				{
					dir += Vector3.forward;
				}
				if (Input.GetKey(KeyCode.S))
				{
					dir += Vector3.back;
				}
				if (Input.GetKey(KeyCode.A))
				{
					dir += Vector3.left;
				}
				if (Input.GetKey(KeyCode.D))
				{
					dir += Vector3.right;
				}
				if (Input.GetKey(KeyCode.LeftShift))
				{
					dir += Vector3.up;
				}
				if (Input.GetKey(KeyCode.LeftControl))
				{
					dir += Vector3.down;
				}
				
				var rot = Vector2.zero;
				if (Input.GetKey(KeyCode.Q))
				{
					rot.y -= 1;
				}
				if (Input.GetKey(KeyCode.E))
				{
					rot.y += 1;
				}

				_xRot += rot.x * Time.deltaTime * lookSpeed;
				_yRot += rot.y * Time.deltaTime * lookSpeed;
				transform.position += Quaternion.AngleAxis(_yRot, Vector3.up) * dir * Time.deltaTime * speed;

				transform.rotation = Quaternion.AngleAxis(_yRot, Vector3.up) * Quaternion.AngleAxis(_xRot, Vector3.left);
			}
		}
	}
}
