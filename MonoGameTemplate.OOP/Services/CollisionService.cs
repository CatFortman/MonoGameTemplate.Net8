using System;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Models;
using MonoGameTemplate.OOP.Entities.Interfaces;

namespace MonoGameTemplate.OOP.Services;

public class CollisionService
{
    public bool Intersects(ICollidable a, ICollidable b)
    {
        return a.Bounds.Intersects(b.Bounds);
    }

    public bool Intersects(Circle a, Circle b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        var radiusSum = a.Radius + b.Radius;

        return (dx * dx + dy * dy) <= radiusSum * radiusSum;
    }

    public bool Intersects(Rectangle rect, Circle circle)
    {
        float closestX = Math.Clamp(circle.X, rect.Left, rect.Right);
        float closestY = Math.Clamp(circle.Y, rect.Top, rect.Bottom);

        float dx = circle.X - closestX;
        float dy = circle.Y - closestY;

        return (dx * dx + dy * dy) <= circle.Radius * circle.Radius;
    }
}