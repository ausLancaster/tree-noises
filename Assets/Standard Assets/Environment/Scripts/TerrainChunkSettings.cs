namespace Terrain
{
    public class TerrainChunkSettings
    {
        public int resolution { get; private set; }

        public float length { get; private set; }
        public float height { get; private set; }

        public TerrainChunkSettings(int resolution, float length, float height)
        {
            this.resolution = resolution;
            this.length = length;
            this.height = height;
        }
    }
}

