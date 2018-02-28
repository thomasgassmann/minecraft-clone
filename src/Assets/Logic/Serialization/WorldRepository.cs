namespace Assets.Logic.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class WorldRepository
    {
        public const string SaveLocation = "saves";

        public static string GetSaveLocation(string worldName)
        {
            var path = Path.Combine(WorldRepository.SaveLocation, worldName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static string GetFileName(WorldPosition chunkLocation)
        {
            return string.Concat(
                "X(",
                chunkLocation.X,
                ")-Y(",
                chunkLocation.Y,
                ")-Z(",
                chunkLocation.Z,
                ").chunk");
        }

        public static string GetChunkSavePath(Chunk chunk)
        {
            var saveFile = WorldRepository.GetSaveLocation(chunk.World.Title);
            return Path.Combine(saveFile, WorldRepository.GetFileName(chunk.Position));
        }

        public static void SaveChunk(Chunk chunk)
        {
            using (var str = new FileStream(
                WorldRepository.GetChunkSavePath(chunk),
                FileMode.Create,
                FileAccess.Write,
                FileShare.None))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(str, chunk.Blocks);
            }
        }

        public static bool LoadChunk(Chunk chunk)
        {
            var path = WorldRepository.GetChunkSavePath(chunk);
            if (!File.Exists(path))
            {
                return false;
            }

            using (var str = new FileStream(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var save = (Block[,,])formatter.Deserialize(str);
                chunk.Blocks = save;
            }

            return true;
        }
    }
}
