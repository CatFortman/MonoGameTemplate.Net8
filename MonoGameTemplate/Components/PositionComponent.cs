﻿using MonoGameTemplate.Entities;

namespace MonoGameTemplate.Components
{
    public class PositionComponent : IComponent
    {
        public int X, Y;

        public PositionComponent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
