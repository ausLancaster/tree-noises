using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Terrain
{

    public class TerrainChunk
    {
        public Vector2i position { get; private set; }

        private Terrain terrain { get; set; }

        private float[,] heightmap { get; set; }

        private TerrainChunkSettings settings { get; set; }

        private INoiseProvider noiseProvider { get; set; }

        private Material material { get; set; }

        private object HeightmapThreadLockObject { get; set; }

        private TerrainChunkNeighborhood Neighborhood { get; set; }

        private GameObject gameObject { get; set; }

        public TerrainChunk(TerrainChunkSettings settings, INoiseProvider noiseProvider, int x, int z, Material material)
        {
            HeightmapThreadLockObject = new object();

            this.settings = settings;
            this.noiseProvider = noiseProvider;
            Neighborhood = new TerrainChunkNeighborhood();

            position = new Vector2i(x, z);
            this.material = material;
        }

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as TerrainChunk;
            if (other == null)
                return false;

            return this.position.Equals(other.position);
        }

        public void CreateTerrain()
        {
            //var heightMap = GetHeightmap();
            terrain = new Terrain(heightmap, settings.length, settings.height, settings.resolution);
            var mesh = terrain.Generate().Generate();

            gameObject = new GameObject("Terrain Chunk (" + position.X + ", " + position.Z + ")");
            gameObject.transform.position = new Vector3(position.X*settings.length, 0, position.Z*settings.length);

            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.AddComponent <MeshCollider>();
            gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            var renderer = gameObject.AddComponent<MeshRenderer>();            
            renderer.material = material;
        }

        public void GenerateHeightmap()
        {
            var thread = new Thread(GenerateHeightmapThread);
            thread.Start();
        }

        private void GenerateHeightmapThread()
        {
            lock (HeightmapThreadLockObject)
            {
                var heightmap = new float[settings.resolution+2, settings.resolution+2];

                for (var xRes = 0; xRes < settings.resolution+2; xRes++)
                {
                    for (var zRes = 0; zRes < settings.resolution+2; zRes++)
                    {
                        var xCoordinate = position.X * (settings.resolution - 1) + (float)xRes-1;
                        var zCoordinate = position.Z * (settings.resolution - 1) + (float)zRes-1;
                        heightmap[xRes, zRes] = noiseProvider.GetValue(xCoordinate, zCoordinate);
                    }
                }
                this.heightmap = heightmap;
            }
        }

        public bool IsHeightmapReady()
        {
            return terrain == null && heightmap != null;
        }

        public float GetTerrainHeight(Vector3 worldPosition)
        {
            return noiseProvider.GetValue(worldPosition.x, worldPosition.z);
        }

        public void Remove()
        {
            heightmap = null;
            settings = null;

            if (Neighborhood.XDown != null)
            {
                Neighborhood.XDown.RemoveFromNeighborhood(this);
                Neighborhood.XDown = null;
            }
            if (Neighborhood.XUp != null)
            {
                Neighborhood.XUp.RemoveFromNeighborhood(this);
                Neighborhood.XUp = null;
            }
            if (Neighborhood.ZDown != null)
            {
                Neighborhood.ZDown.RemoveFromNeighborhood(this);
                Neighborhood.ZDown = null;
            }
            if (Neighborhood.ZUp != null)
            {
                Neighborhood.ZUp.RemoveFromNeighborhood(this);
                Neighborhood.ZUp = null;
            }

            if (terrain != null)
                GameObject.Destroy(gameObject);
        }

        public void RemoveFromNeighborhood(TerrainChunk chunk)
        {
            if (Neighborhood.XDown == chunk)
                Neighborhood.XDown = null;
            if (Neighborhood.XUp == chunk)
                Neighborhood.XUp = null;
            if (Neighborhood.ZDown == chunk)
                Neighborhood.ZDown = null;
            if (Neighborhood.ZUp == chunk)
                Neighborhood.ZUp = null;
        }

        public void SetNeighbors(TerrainChunk chunk, TerrainNeighbor direction)
        {
            if (chunk != null)
            {
                switch (direction)
                {
                    case TerrainNeighbor.XUp:
                        Neighborhood.XUp = chunk;
                        break;

                    case TerrainNeighbor.XDown:
                        Neighborhood.XDown = chunk;
                        break;

                    case TerrainNeighbor.ZUp:
                        Neighborhood.ZUp = chunk;
                        break;

                    case TerrainNeighbor.ZDown:
                        Neighborhood.ZDown = chunk;
                        break;
                }
            }
        }

        public void UpdateNeighbors()
        {
            if (terrain != null)
            {
                var xDown = Neighborhood.XDown == null ? null : Neighborhood.XDown.terrain;
                var xUp = Neighborhood.XUp == null ? null : Neighborhood.XUp.terrain;
                var zDown = Neighborhood.ZDown == null ? null : Neighborhood.ZDown.terrain;
                var zUp = Neighborhood.ZUp == null ? null : Neighborhood.ZUp.terrain;
                //terrain.SetNeighbors(xDown, zUp, xUp, zDown);
                //terrain.Flush();
            }
        }


    }


}

