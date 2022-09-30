namespace FuelFans.Models
{
    public class RouteResult
    {
        public RouteResultFeature[] features { get; set; }
        public RouteResultProperties properties { get; set; }
        public string type { get; set; }
    }

    public class RouteResultProperties
    {
        public string mode { get; set; }
        public Waypoint[] waypoints { get; set; }
        public string units { get; set; }
    }

    public class Waypoint
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

    public class RouteResultFeature
    {
        public string type { get; set; }
        public Properties1 properties { get; set; }
        public RouteResultGeometry geometry { get; set; }
    }

    public class Properties1
    {
        public string mode { get; set; }
        public Waypoint1[] waypoints { get; set; }
        public string units { get; set; }
        public int distance { get; set; }
        public string distance_units { get; set; }
        public float time { get; set; }
        public Leg[] legs { get; set; }
    }

    public class Waypoint1
    {
        public float[] location { get; set; }
        public int original_index { get; set; }
    }

    public class Leg
    {
        public int distance { get; set; }
        public float time { get; set; }
        public Step[] steps { get; set; }
    }

    public class Step
    {
        public int from_index { get; set; }
        public int to_index { get; set; }
        public float distance { get; set; }
        public float time { get; set; }
        public Instruction instruction { get; set; }
    }

    public class Instruction
    {
        public string text { get; set; }
    }

    public class RouteResultGeometry
    {
        public string type { get; set; }
        public float[][][] coordinates { get; set; }
    }
}
