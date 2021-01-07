﻿using UnityEngine;

namespace Common.Prototyping
{
	public abstract class ProceduralMeshBase : MonoBehaviour, IProceduralMesh
	{
		public bool IsDirty { get; set; }

		public abstract IMeshBuilder Create();

#if UNITY_EDITOR
		private void OnValidate()
		{
			IsDirty = true;
		}
#endif
	}
}
