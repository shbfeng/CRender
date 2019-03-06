﻿namespace CRender.Math
{
    public partial struct Matrix4x4
    {
        private struct RotationData
        {
            public Vector3 sinXYZ, cosXYZ;

            public float sinXsinY, cosXsinY, sinXsinZ, cosXsinZ;

            public RotationData(Vector3 rotation)
            {
                sinXYZ = JMath.Sin(rotation);
                cosXYZ = JMath.Cos(rotation);
                sinXsinY = sinXYZ.X * sinXYZ.Y;
                cosXsinY = cosXYZ.X * sinXYZ.Y;
                sinXsinZ = sinXYZ.X * sinXYZ.Z;
                cosXsinZ = cosXYZ.X * sinXYZ.Z;
            }
        }

        public static Matrix4x4 Translation(Vector3 vector) => new Matrix4x4(
            m11: 1, m21: 0, m31: 0, m41: vector.X,
            m12: 0, m22: 1, m32: 0, m42: vector.Y,
            m13: 0, m23: 0, m33: 1, m43: vector.Z,
            m14: 0, m24: 0, m34: 0, m44: 1);

        public static Matrix4x4 Rotation(Vector3 rotation)
        {
            RotationData rot = new RotationData(rotation);

            //It's really complex!
            return new Matrix4x4(
                m11: rot.cosXYZ.Y * rot.cosXYZ.Z,
                m21: (rot.sinXsinY - rot.cosXsinZ) * rot.sinXYZ.Z,
                m31: (rot.cosXsinY * rot.cosXYZ.Z + rot.sinXsinZ),
                m12: rot.cosXYZ.Y * rot.sinXYZ.Z,
                m22: (rot.cosXYZ.X * rot.cosXYZ.Z + rot.sinXsinY * rot.sinXYZ.Z),
                m32: -rot.sinXYZ.X * rot.sinXYZ.Z,
                m13: -rot.sinXYZ.Y,
                m23: rot.sinXsinY,
                m33: rot.cosXYZ.X * rot.cosXYZ.Y,
                m14: 0, m24: 0, m34: 0,
                m41: 0, m42: 0, m43: 0, m44: 1);
        }

        public static Matrix4x4 Scale(Vector3 scale) => new Matrix4x4(
                m11: scale.X, m21: 0, m31: 0, m41: 0,
                m12: 0, m22: scale.Y, m32: 0, m42: 0,
                m13: 0, m23: 0, m33: scale.Z, m43: 0,
                m14: 0, m24: 0, m34: 0, m44: 1);

        public static Matrix4x4 Scale(float scaleX, float scaleY, float scaleZ) => new Matrix4x4(
                m11: scaleX, m21: 0, m31: 0, m41: 0,
                m12: 0, m22: scaleY, m32: 0, m42: 0,
                m13: 0, m23: 0, m33: scaleZ, m43: 0,
                m14: 0, m24: 0, m34: 0, m44: 1);

        public static Matrix4x4 ScaleRotationTranslation(Vector3 scale, Vector3 rotation, Vector3 translation)
        {
            RotationData rot = new RotationData(rotation);

            return new Matrix4x4(
                m11: rot.cosXYZ.Y * rot.cosXYZ.Z,
                m21: (rot.sinXsinY - rot.cosXsinZ) * rot.sinXYZ.Z * scale.Y,
                m31: (rot.cosXsinY * rot.cosXYZ.Z + rot.sinXsinZ) * scale.Z,
                m41: translation.X,
                m12: rot.cosXYZ.Y * rot.sinXYZ.Z * scale.X,
                m22: (rot.cosXYZ.X * rot.cosXYZ.Z + rot.sinXsinY * rot.sinXYZ.Z) * scale.Y,
                m32: -rot.sinXYZ.X * rot.sinXYZ.Z * scale.Y,
                m42: translation.Y,
                m13: -rot.sinXYZ.Y * scale.X,
                m23: rot.sinXsinY * scale.Z,
                m33: rot.cosXYZ.X * rot.cosXYZ.Y * scale.Z,
                m43: translation.Z,
                m14: 0, m24: 0, m34: 0, m44: 1);
        }

        /// <summary>
        /// Positive X oriented, Y is <paramref name="width"/>,Z is <paramref name="height"/>
        /// </summary>
        public static Matrix4x4 Orthographic(float width, float height, float near, float far)
        {
            float scaleX = 2f / (far - near);
            return new Matrix4x4(
                m11: scaleX, m21: 0, m31: 0, m41: -(near + far) * .5f * scaleX,
                m12: 0, m22: 2f / height, m32: 0, m42: 0,
                m13: 0, m23: 0, m33: 2f / width, m43: 0,
                m14: 0, m24: 0, m34: 0, m44: 1);
        }
    }
}