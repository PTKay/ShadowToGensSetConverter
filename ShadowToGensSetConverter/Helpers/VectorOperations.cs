using ShadowToGensSetConverter.SetObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowToGensSetConverter.Helpers
{
    public class VectorOperations
    {
        public static Position CalculateNextPosition(Position currentPosition, Rotation rotation, float distance)
        {
            // Extracting X, Y, and Z components of the current position
            float x = currentPosition.x;
            float y = currentPosition.y;
            float z = currentPosition.z;

            // Convert rotation angles from degrees to radians
            double rotationPitch = rotation.x * Math.PI / 180.0;
            double rotationYaw = rotation.y * Math.PI / 180.0;
            double rotationRoll = rotation.z * Math.PI / 180.0;

            double dirX = Math.Sin(rotationYaw) * Math.Cos(rotationPitch);
            double dirY = Math.Sin(rotationPitch);
            double dirZ = Math.Cos(rotationYaw) * Math.Cos(rotationPitch);

            // Calculate the new position
            double newX = x + (distance * dirX);
            double newY = y - (distance * dirY);
            double newZ = z + (distance * dirZ);

            // Return the new position as a vector
            return new Position((float)newX, (float)newY, (float)newZ);
        }

        public static Rotation ToQuaternion(Rotation oldRotation)
        {
            double rotationPitch = oldRotation.x * Math.PI / 180.0;
            double rotationYaw = oldRotation.y * Math.PI / 180.0;
            double rotationRoll = oldRotation.z * Math.PI / 180.0;

            double cy = Math.Cos(rotationYaw * 0.5);
            double sy = Math.Sin(rotationYaw * 0.5);
            double cp = Math.Cos(rotationPitch * 0.5);
            double sp = Math.Sin(rotationPitch * 0.5);
            double cr = Math.Cos(rotationRoll * 0.5);
            double sr = Math.Sin(rotationRoll * 0.5);

            Rotation rot = new Rotation();
            rot.w = (float)(cr * cp * cy + sr * sp * sy);
            rot.x = (float)(cr * sp * cy + sr * cp * sy);
            rot.y = (float)(cr * cp * sy - sr * sp * cy);
            rot.z = (float)(sr * cp * cy - cr * sp * sy);

            return rot;
        }
    }
}
