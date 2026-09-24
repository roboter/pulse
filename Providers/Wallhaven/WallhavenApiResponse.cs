using System.Collections.Generic;

namespace wallhaven
{
    public class WallhavenApiResponse
    {
        public List<WallhavenWallpaper> data { get; set; }
        public WallhavenMeta meta { get; set; }

        public WallhavenApiResponse()
        {
            data = new List<WallhavenWallpaper>();
        }
    }

    public class WallhavenWallpaper
    {
        public string id { get; set; }
        public string url { get; set; }
        public string short_url { get; set; }
        public int views { get; set; }
        public int favorites { get; set; }
        public string source { get; set; }
        public string purity { get; set; }
        public string category { get; set; }
        public int dimension_x { get; set; }
        public int dimension_y { get; set; }
        public string resolution { get; set; }
        public string ratio { get; set; }
        public long file_size { get; set; }
        public string file_type { get; set; }
        public string created_at { get; set; }
        public string[] colors { get; set; }
        public string path { get; set; }
        public WallhavenThumbs thumbs { get; set; }
    }

    public class WallhavenThumbs
    {
        public string large { get; set; }
        public string original { get; set; }
        public string small { get; set; }
    }

    public class WallhavenMeta
    {
        public int current_page { get; set; }
        public int last_page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public string seed { get; set; }
    }
}
