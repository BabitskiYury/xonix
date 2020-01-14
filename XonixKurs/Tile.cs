using SFML.Graphics;
using SFML.System;

namespace SFML
{
    

    enum TileType
    {
        NONE,
        BLUE,
        RED,
        GREEN
    }

    class Tile : Transformable,Drawable
    {
        public const int TILE_SIZE = 18;

        RectangleShape rectShape;
        public TileType type;

        public Tile(TileType a)
        {
            type = new TileType();
            type = TileType.NONE;
            rectShape = new RectangleShape(new System.Vector2f(TILE_SIZE, TILE_SIZE));
            switch (a)
            {
                case TileType.BLUE:
                    type = TileType.BLUE;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.RED:
                    type = TileType.RED;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(36, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.GREEN:
                    type = TileType.GREEN;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(54, 0, TILE_SIZE, TILE_SIZE);
                    break;
                default:
                    break;
            }
        }
        public Tile(TileType a,Vector2f position)
        {
            type = new TileType();
            type = TileType.NONE;
            rectShape = new RectangleShape(new System.Vector2f(TILE_SIZE, TILE_SIZE));
            rectShape.Position = position;
            switch (a)
            {
                case TileType.BLUE:
                    type = TileType.BLUE;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.RED:
                    type = TileType.RED;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(36, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.GREEN:
                    type = TileType.GREEN;
                    rectShape.Texture = Content.tiles;
                    rectShape.TextureRect = new IntRect(54, 0, TILE_SIZE, TILE_SIZE);
                    break;
                default:
                    break;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            target.Draw(rectShape, states);
        }
    }
}