using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Plane {

	// Use this for initialization
	public static Mesh Mesh(float radius, float length, int segments)
    {
        float angle = 360.0f / segments;

        // Vertices
        Vector3[] vertices = new Vector3[segments * 2 + 2];
        int bottom = vertices.Length - 2;
        int top = vertices.Length - 1;

        for (int i=0; i<segments; i++)
        {
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            vertices[i + segments] = new Vector3(radius * Mathf.Cos(angle), length, radius * Mathf.Sin(angle));
        }

        vertices[bottom] = new Vector3(0, 0, 0);
        vertices[top] = new Vector3(0, length, 0);

        // Triangles
        int[] triangles = new int[segments * 4 * 3];

        for (int i=0; i<segments; i++)
        {
            int v0 = i;
            int v1 = i + 1; if (v1 == segments) v1 = 0;
            int v2 = i + segments;
            int v3 = i + segments + 1; if (v1 == segments*2) v1 = segments;

            triangles[i * 12] = v0;
            triangles[i * 12 + 1] = bottom;
            triangles[i * 12 + 2] = v1;

            triangles[i * 12 + 3] = v2;
            triangles[i * 12 + 4] = v3;
            triangles[i * 12 + 5] = top;

            triangles[i * 12 + 6] = bottom;
            triangles[i * 12 + 7] = bottom;
            triangles[i * 12 + 8] = bottom;

            triangles[i * 12 + 9] = bottom;
            triangles[i * 12 + 10] = bottom;
            triangles[i * 12 + 11] = bottom;

        }



        // Normals

        // UVs

        return null;
    }
}
