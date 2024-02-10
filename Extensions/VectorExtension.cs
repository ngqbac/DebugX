using UnityEngine;

namespace DebugX.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 Multiply(this Vector3 vector, float scalar)
        {
            return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }
        
        public static Vector2 Multiply(this Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        
        public static Vector3 Multiply(this Vector3 vector, Vector3 multiplyVector)
        {
            return new Vector3(vector.x * multiplyVector.x, vector.y * multiplyVector.y, vector.z * multiplyVector.z);
        }
        
        public static Vector2 Multiply(this Vector2 vector, Vector2 multiplyVector)
        {
            return new Vector2(vector.x * multiplyVector.x, vector.y * multiplyVector.y);
        }

        public static Vector3 Add(this Vector3 vector, float valueToAdd)
        {
            return new Vector3(vector.x + valueToAdd, vector.y + valueToAdd, vector.z + valueToAdd);
        }
        
        public static Vector2 Add(this Vector2 vector, float valueToAdd)
        {
            return new Vector2(vector.x + valueToAdd, vector.y + valueToAdd);
        }
        
        public static Vector3 Add(this Vector3 vector, Vector3 addVector)
        {
            return new Vector3(vector.x + addVector.x, vector.y + addVector.y, vector.z + addVector.z);
        }
        
        public static Vector2 Add(this Vector2 vector, Vector2 addVector)
        {
            return new Vector2(vector.x + addVector.x, vector.y + addVector.y);
        }

        public static Vector3 Subtract(this Vector3 vector, float valueToAdd)
        {
            return new Vector3(vector.x - valueToAdd, vector.y - valueToAdd, vector.z - valueToAdd);
        }
        
        public static Vector2 Subtract(this Vector2 vector, float valueToAdd)
        {
            return new Vector3(vector.x - valueToAdd, vector.y - valueToAdd);
        }

        public static Vector3 Subtract(this Vector3 vector, Vector3 subtractVector)
        {
            return new Vector3(vector.x - subtractVector.x, vector.y - subtractVector.y, vector.z - subtractVector.z);
        }
        
        public static Vector2 Subtract(this Vector2 vector, Vector2 subtractVector)
        {
            return new Vector2(vector.x - subtractVector.x, vector.y - subtractVector.y);
        }

        public static Vector3 Divine(this Vector3 vector, float divisor)
        {
            return divisor != 0f
                ? new Vector3(vector.x / divisor, vector.y / divisor, vector.z / divisor)
                : Vector3.zero;
        }
        
        public static Vector2 Divine(this Vector2 vector, float divisor)
        {
            return divisor != 0f
                ? new Vector2(vector.x / divisor, vector.y / divisor)
                : Vector3.zero;
        }
    }
}