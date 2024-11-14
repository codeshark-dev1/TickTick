using System;
using System.ComponentModel.Design;
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
        public int levelTimer = 30;

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

            float clampedX = 0;
            float clampedY = 0;

            if (targetObject != null && target.X * 0.9f < worldSize.X / 2)
            {
                clampedX = MathHelper.Clamp(target.X - windowSize.X / 2, 0, target.X);
                clampedY = 0;
            }else if ( targetObject != null && target.X * 0.9f >= worldSize.X / 2)
            {
                clampedX = worldSize.X / 4;
                clampedY = 0;
            }
            else
            {
                clampedX = 0;
                clampedY = 0;
            }
            Vector2 newPosition = new Vector2(clampedX, clampedY);

            position = Vector2.Lerp(position.ToVector2(), newPosition, smoothSpeed).ToPoint();
        }

        public void SetTarget(Vector2 newTarget, GameObject obj)
        {
            target = newTarget;
            targetObject = obj;
        }

        public void InitializeLevel(int width, int height, int time)
        {
            worldSize.X = width;
            worldSize.Y = height;
            levelTimer = time; 
        }

        public Matrix GetTransformMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)); //returns the translation matrix by which the camera should be off set to only display the part of the screen we want to display
        }

        public void Reset()
        {
            worldSize = Point.Zero;
            levelTimer = 0;
            position = Point.Zero;
            targetObject = null;
            target = Vector2.Zero;
        }
    }
}