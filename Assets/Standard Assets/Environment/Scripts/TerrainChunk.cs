using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace Terrain
{

    public class TerrainChunk
    {
        private Terrain terrain { get; set; }

        private float[,] heightmap { get; set; }

        private TerrainChunkSettings settings { get; set; }

        private INoiseProvider noiseProvider { get; set; }

        private List<IFeatureGenerator> featureGenerator;

        private Material material { get; set; }

        private float seed;


        private GameObject gameObject { get; set; }

        public TerrainChunk(TerrainChunkSettings settings, INoiseProvider noiseProvider, List<IFeatureGenerator> featureGenerator, Material material)
        {
            this.settings = settings;
            this.noiseProvider = noiseProvider;
            this.featureGenerator = featureGenerator;
            this.material = material;

            seed = Random.Range(0.0f, 100000.0f);
        }

        public void CreateTerrain()
        {
            //var heightMap = GetHeightmap();

            terrain = new Terrain(heightmap, settings.length, settings.height, settings.resolution);

            var meshBuilder = terrain.Generate();
            var mesh = meshBuilder.Generate();

            gameObject = new GameObject("Terrain Chunk");
            gameObject.transform.position = new Vector3(0, 0, 0);

            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.AddComponent <MeshCollider>();
            gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            var renderer = gameObject.AddComponent<MeshRenderer>();            
            renderer.material = material;

            for (int i=0; i<featureGenerator.Count; i++)
            {
                //featureGenerator[i].Generate(gameObject.transform.position, noiseProvider);
            }

        }

        public void GenerateHeightmap()
        {
            var heightmap = new float[settings.resolution, settings.resolution];

            for (var xRes = 0; xRes < settings.resolution; xRes++)
            {
                for (var zRes = 0; zRes < settings.resolution; zRes++)
                {
                    var xCoordinate = ((float)xRes / settings.resolution) * settings.length - settings.length / 2;
                    var zCoordinate = ((float)zRes / settings.resolution) * settings.length - settings.length / 2;

                    heightmap[xRes, zRes] = noiseProvider.GetValue(xCoordinate, zCoordinate, seed);
                }
            }
            this.heightmap = heightmap;
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        public float GetTerrainHeight(float x, float z)
        {
            return noiseProvider.GetValue(x, z, seed);
        }
    }


}

