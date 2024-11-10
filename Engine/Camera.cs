using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Camera
    {

        public Point position, windowSize, worldSize; //world size is total size of the level, set by ExtendedGame class, windowSize is the size of the world currently being displayed
        private Point startPosition = Point.Zero;
        private static Camera instance;

        private GameObject targetObject;
        public Vector2 target;

        private float sizeXMultiplier = .5f, sizeYMultiplier = .5f, smoothSpeed = 0.1f;

        private Camera()
        {
            position = startPosition;
        }

        public static Camera Instance //returns instance of camera, making sure there is only one at all times
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }

                return instance;
            }
        }

        public void Update(GameTime gameTime)
        {
            windowSize = new Point((int)(worldSize.X * sizeXMultiplier), (int)(worldSize.Y * sizeYMultiplier));

            float clampedX = MathHelper.Clamp(target.X - windowSize.X / 2, 0, Math.Max(0, worldSize.X - windowSize.X));
            float clampedY = MathHelper.Clamp(target.Y - windowSize.Y / 2, 0, Math.Min(0, -(worldSize.Y - windowSize.Y)));

            Vector2 newPosition = new Vector2(clampedX, clampedY);

            position = Vector2.Lerp(position.ToVector2(), newPosition, smoothSpeed).ToPoint();
            //Debug.WriteLine($"Position: {position}, target: {target}, moving towards: {newPosition} ");
        }

        public void SetTarget(Vector2 newTarget, GameObject obj)
        {
            target = newTarget;
            targetObject = obj;
        }

        public Matrix GetTransformMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)); //returns the translation matrix by which the camera should be off set to only display the part of the screen we want to display
        }
    }
}