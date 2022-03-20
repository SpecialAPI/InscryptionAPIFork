using System;
using System.Runtime.InteropServices;

namespace InscryptionAPI.Prefabs
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct EditorVector3 : IEquatable<EditorVector3>
    {
        public float X;
        public float Y;
        public float Z;

        public EditorVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Invalid EditorVector3 index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Invalid EditorVector3 index!");
                }
            }
        }

        public override string ToString()
        {
            return $"(X:{X},Y:{Y},Z:{Z})";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2) ^ (Z.GetHashCode() >> 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is EditorVector3))
                return false;
            return Equals((EditorVector3)other);
        }

        public bool Equals(EditorVector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public void Normalize()
        {
            var length = Length();
            if (length > kEpsilon)
            {
                var invNorm = 1.0f / length;
                X *= invNorm;
                Y *= invNorm;
                Z *= invNorm;
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }

        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public static EditorVector3 Zero => new EditorVector3();

        public static EditorVector3 One => new EditorVector3(1.0f, 1.0f, 1.0f);

        public static EditorVector3 operator +(EditorVector3 a, EditorVector3 b)
        {
            return new EditorVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static EditorVector3 operator -(EditorVector3 a, EditorVector3 b)
        {
            return new EditorVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static EditorVector3 operator -(EditorVector3 a)
        {
            return new EditorVector3(-a.X, -a.Y, -a.Z);
        }

        public static EditorVector3 operator *(EditorVector3 a, float d)
        {
            return new EditorVector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static EditorVector3 operator *(float d, EditorVector3 a)
        {
            return new EditorVector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static EditorVector3 operator /(EditorVector3 a, float d)
        {
            return new EditorVector3(a.X / d, a.Y / d, a.Z / d);
        }

        public static bool operator ==(EditorVector3 lhs, EditorVector3 rhs)
        {
            return (lhs - rhs).LengthSquared() < kEpsilon * kEpsilon;
        }

        public static bool operator !=(EditorVector3 lhs, EditorVector3 rhs)
        {
            return !(lhs == rhs);
        }

        /*public static implicit operator Vector2(EditorVector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator Vector4(EditorVector3 v)
        {
            return new Vector4(v.X, v.Y, v.Z, 0.0F);
        }*/

        private const float kEpsilon = 0.00001F;
    }
}