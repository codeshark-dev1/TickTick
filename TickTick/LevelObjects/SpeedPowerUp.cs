using Engine;
using Microsoft.Xna.Framework;
using System;

class SpeedPowerUp : AnimatedGameObject
{
    Level level;
    Vector2 startPosition;
    bool triggered;
    double timeLeft;

    const double time = 5;

    public SpeedPowerUp(Level level, Vector2 startPosition)
        : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;

        LoadAnimation("Sprites/LevelObjects/SpeedPowerUp/spr_speedpowerup", "speedpowerup", true, 0.1f);
        PlayAnimation("speedpowerup");
        SetOriginToCenter();
        Reset();
    }

    public override void Reset()
    {
        // go back to the starting position
        triggered = false;
        this.Visible = true;
        LocalPosition = startPosition;
        timeLeft = time;
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //if the player touches the powerup it speed inceases
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            this.Visible = false;
            if (!triggered)
            {
                level.Player.SpeedUp(2);
            }
        }
        if (timeLeft > 0)
        {
            timeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (timeLeft < 0)
        {
            level.Player.SpeedUp(1);
        }

    }
}