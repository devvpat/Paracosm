using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class CameraFollow : MonoBehaviour
    {
		[SerializeField]
		private Transform PlayerTransform;

		//Limits for camera (optional)
		[SerializeField]
		private float RightLimit;
		[SerializeField]
		private float LeftLimit;
		[SerializeField]
		private float UpperLimit;
		[SerializeField]
		private float LowerLimit;

		//Public so if we need to change this somewhere we can do so easily
		public bool FollowPlayer;

		void Start()
		{
			if (RightLimit < LowerLimit || UpperLimit < LowerLimit) FollowPlayer = false;
		}

		void Update()
		{
			if (FollowPlayer)
			{
				Vector2 playerPos = PlayerTransform.position;
				Vector2 curPos = transform.position;
				if (playerPos.x < LeftLimit) curPos.x = LeftLimit;
				else if (playerPos.x > RightLimit) curPos.x = RightLimit;
				if (playerPos.y < LowerLimit) curPos.y = LowerLimit;
				else if (playerPos.y > UpperLimit) curPos.y = UpperLimit;
				transform.position = curPos;
			}
		}
	}
}
