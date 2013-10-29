using UnityEngine;
using System.Collections;
using Leap;

public class Spawner : MonoBehaviour
{
	public GameObject unit;
	public GameObject[] fingerTips;

	private Leap.Controller controller;
	private float coolDown = 0;

	void Start()
	{
		controller = new Controller();
		controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
	}

	void Update()
	{
		coolDown -= Time.deltaTime;
		Frame frame = controller.Frame();
		if (coolDown <= 0)
		{
			foreach (Gesture gesture in frame.Gestures())
			{
				switch (gesture.Type)
				{
					case Gesture.GestureType.TYPEKEYTAP:
						KeyTapGesture keyTap = new KeyTapGesture(gesture);
						Vector3 pos = new Vector3(keyTap.Position.x, keyTap.Position.y, 0);
						GameObject.Instantiate(unit, pos, Quaternion.identity);
						coolDown = 0.2f;
						break;
					default:
						break;
				}
			}
		}
		int i = 0;
		foreach (Finger finger in frame.Fingers)
		{
			if (i >= fingerTips.Length)
				break;
			fingerTips[i].SetActive(true);
			fingerTips[i].transform.position = new Vector3(finger.TipPosition.x, finger.TipPosition.y, -finger.TipPosition.z);
			fingerTips[i].transform.rotation = Quaternion.LookRotation(new Vector3(finger.Direction.x, finger.Direction.y, -finger.Direction.z));
			fingerTips[i].transform.localScale = new Vector3(finger.Width, finger.Width, finger.Length);
			i++;
		}
		for (; i < fingerTips.Length; ++i)
		{
			fingerTips[i].SetActive(false);
		}
	}
}
