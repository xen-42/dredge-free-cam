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

		public void Awake()
		{
			if (Instance != null)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
				camera = gameObject.AddComponent<Camera>();
			}
		}

		public void Update()
		{
			if (Input.GetKeyUp(KeyCode.Semicolon))
			{
				IsActive = !IsActive;
				if (IsActive)
				{
					oldCamera = Camera.current;
					oldCamera.enabled = false;
					camera.enabled = true;
				}
				else
				{
					camera.enabled = false;
					oldCamera.enabled = true;
				}
			}

			if (IsActive)
			{
				if (Input.GetKeyUp(KeyCode.W))
				{
					transform.position += Vector3.forward;
				}
				if (Input.GetKeyUp(KeyCode.S))
				{
					transform.position += Vector3.back;
				}
				if (Input.GetKeyUp(KeyCode.A))
				{
					transform.position += Vector3.left;
				}
				if (Input.GetKeyUp(KeyCode.D))
				{
					transform.position += Vector3.right;
				}
			}
		}
	}
}
