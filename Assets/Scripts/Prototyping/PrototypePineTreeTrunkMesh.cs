﻿using Common.Mathematics;
using Common.Rendering;
using System;
using UnityEngine;

namespace Common.Prototyping
{
	public class PrototypePineTreeTrunkMesh : PrototypeMeshBase
	{
		[Header("Properties")]
		public Input input = Input.Default;

		public override IMeshData Create()
		{
			return Create(in input);
		}

		public static FlatMeshDataUVs Create(in Input input)
		{
			var meshData = new FlatMeshDataUVs();

			var vt = new Vector3(0.0f, input.height, 0.0f);
			var vr = new Vector3(0.0f, input.height * input.rootThreshold, 0.0f);

			var invHeight = 1f / input.height;

			var uv0 = new Vector2(0.0f, 0.0f);
			var uv1 = new Vector2(1.0f, 0.0f);

			var uv2 = new Vector2(0.0f, vr.y * invHeight);
			var uv3 = new Vector2(1.0f, vr.y * invHeight);

			var uvt = new Vector2(0.5f, 1.0f);

			var vertex0 = Geometry.Vertex(input.vertices - 1, input.vertices);
			for (int i = 0; i < input.vertices; ++i)
			{
				var vertex1 = Geometry.Vertex(i, input.vertices);

				var v0 = vertex0 * input.radius * (1.0f + input.rootRadius);
				var v1 = vertex1 * input.radius * (1.0f + input.rootRadius);

				var v2 = vertex0 * input.radius + vr;
				var v3 = vertex1 * input.radius + vr;

				meshData.AddTriangle(v0, v2, v3);
				meshData.AddTriangle(v0, v3, v1);

				meshData.AddTriangle(v2, vt, v3);

				meshData.AddUVs(uv0, uv2, uv3);
				meshData.AddUVs(uv0, uv3, uv1);

				meshData.AddUVs(uv2, uvt, uv3);

				vertex0 = vertex1;
			}

			return meshData;
		}

		[Serializable]
		public struct Input
		{
			[Range(0.0f, 10.0f)] public float height;
			[Range(0, 10)] public int vertices;
			[Range(0.0f, 2.0f)] public float radius;
			[Range(0.0f, 1.0f)] public float rootRadius;
			[Range(0.0f, 1.0f)] public float rootThreshold;

			public static readonly Input Default = new Input
			{
				height = 2.5f,
				vertices = 4,
				radius = 0.2f,
				rootRadius = 0.5f,
				rootThreshold = 0.05f,
			};
		}
	}
}
