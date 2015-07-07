using Graphics.Extensions;

namespace Graphics.Math
{
    public struct Vector4
    {
        public Vector4(double x, double y, double z, double w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
            _magnitude = System.Math.Sqrt(x * x + y * y + z * z);
        }

        //TODO: check W default value for default vectors
        public static Vector4 I = new Vector4(1, 0, 0, 1);
        public static Vector4 J = new Vector4(0, 1, 0, 1);
        public static Vector4 K = new Vector4(0, 0, 1, 1);
        public static Vector4 Zero = new Vector4(0, 0, 0, 1);

        private readonly double _x;
        public double X { get { return _x; } }

        private readonly double _y;
        public double Y { get { return _y; } }

        private readonly double _z;
        public double Z { get { return _z; } }
        
        //If w == 1, then the vector (x,y,z,1) is a position in space.
        //If w == 0, then the vector (x,y,z,0) is a direction
        private readonly double _w;
        public double W { get { return _w; } }

        private readonly double _magnitude;
        public double Magnitude { get { return _magnitude; } }

        public Vector4 Normalize()
        {
            return new Vector4(_x / _magnitude,
                               _y / _magnitude,
                               _z / _magnitude,
                               _w);
        }

        public Vector4 Cross(Vector4 v2)
        {
            var x = this.Y * v2.Z - this.Z * v2.Y;
            var y = this.Z * v2.X - this.X * v2.Z;
            var z = this.X * v2.Y - this.Y * v2.X;

            return new Vector4(x, y, z, 1);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(_x, _y, _z);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector4))
                return false;

            var vector = (Vector4)obj;

            return _x.IsEqualsTo(vector.X) &&
                   _y.IsEqualsTo(vector.Y) &&
                   _z.IsEqualsTo(vector.Z) &&
                   _w.IsEqualsTo(vector.W);
        }

        public override int GetHashCode()
        {
            return (int)(_x * 17d +
                         _y * 17d +
                         _z * 17d +
                         _w * 17d) +
                         base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(MathHelper.Round(_x), ", ",
                                 MathHelper.Round(_y), ", ", 
                                 MathHelper.Round(_z), ", ", 
                                 MathHelper.Round(_w));
        }

        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;
            double z = v1.Z + v2.Z;
            double w = v1.W;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            double x = v1.X - v2.X;
            double y = v1.Y - v2.Y;
            double z = v1.Z - v2.Z;
            double w = v1.W;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            double x = v1.X * v2.X;
            double y = v1.Y * v2.Y;
            double z = v1.Z * v2.Z;
            double w = v1.W;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 operator *(Vector4 v, double factor)
        {
            return new Vector4(v.X * factor,
                               v.Y * factor,
                               v.Z * factor,
                               v.W);
        }
    }
}