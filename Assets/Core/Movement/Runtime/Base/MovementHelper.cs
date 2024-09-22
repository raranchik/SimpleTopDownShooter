using Core.Base.VContainerExt.Attributes;
using Core.Infrastructure.Start;
using UnityEngine;

namespace Core.Movement.Base
{
    [Register, Context(typeof(StartContext))]
    public class MovementHelper
    {
        private const float CircleDegrees = 360f;

        public Vector3 CalcSmoothPos(in Vector3 pos, in Vector3 dir, in float speed, in float deltaTime)
        {
            return pos + dir * speed * deltaTime;
        }

        public Quaternion CalcSmoothRot(in Quaternion from, in Vector3 dir, in float speed,
            in float deltaTime)
        {
            var to = CalcImmediatelyRot(dir);
            var maxDegreesDelta = speed * CircleDegrees * deltaTime;
            return Quaternion.RotateTowards(from, to, maxDegreesDelta);
        }

        public Quaternion CalcImmediatelyRot(in Vector3 dir)
        {
            var dir3 = new Vector3(dir.x, dir.y);
            return Quaternion.LookRotation(dir3, MovementDefinition.AxisUp2D);
        }
    }
}