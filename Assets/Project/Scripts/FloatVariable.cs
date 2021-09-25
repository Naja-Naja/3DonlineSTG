using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
	public string Name = null;
	public float Value = 0;
	[System.NonSerialized]
	public float RuntimeValue;
	public void init()
	{
		RuntimeValue = Value;
	}
}
