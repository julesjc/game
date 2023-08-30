
using SFML.System;

namespace Game
{
    static class MapUtils
    {
        public static List<SceneSprite> GetGeneratedMap(Vector2f playerPos)
        {
            Vector2f tileSize = new Vector2f(128, 128);

            Vector2u windowSize = App.screenSize;

            List<SceneSprite> generatedMap = new List<SceneSprite>();

            // Number of tiles in the x and y directions
            int maxTilesX = (int)Math.Ceiling(windowSize.X / tileSize.X);
            int maxTilesY = (int)Math.Ceiling(windowSize.Y / tileSize.Y);

            // Create a 2D array to represent the map
            Tile[,] map = new Tile[maxTilesX, maxTilesY];

            // Fill the map with wall tiles
            for (int x = 0; x < maxTilesX; x++)
            {
                for (int y = 0; y < maxTilesY; y++)
                {
                    map[x, y] = new Tile();

                    // Calculate the position based on the center of the tile
                    float xPos = x * tileSize.X + tileSize.X / 2;
                    float yPos = y * tileSize.Y + tileSize.Y / 2;

                    map[x, y].SetPos(new Vector2f(xPos, yPos));

                    generatedMap.Add(map[x, y]);
                }
            }

            Vector2i playerTilePos = new Vector2i(
                  (int)(playerPos.X / tileSize.X),
                  (int)(playerPos.Y / tileSize.Y)
              );

            // Calculate the player's tile bounds
            Vector2i pathTilePos = playerTilePos;

            generatedMap.Remove(map[pathTilePos.X, pathTilePos.Y]);

            bool exitTileSet = false;

            int minSteps = 50;
            // Stop when tile to remove is out of the screen
            while (!exitTileSet)
            {
                // Generate a random direction (up, down, left, or right)
                int direction = RandomUtils.Next(4); // 0: up, 1: down, 2: left, 3: right

                // Calculate the new tile position based on the chosen direction
                Vector2i newTilePos = pathTilePos;
                switch (direction)
                {
                    case 0: newTilePos.Y -= 1; break;
                    case 1: newTilePos.Y += 1; break;
                    case 2: newTilePos.X -= 1; break;
                    case 3: newTilePos.X += 1; break;
                }
                if (newTilePos.X >= 0 && newTilePos.X <= maxTilesX - 1 && newTilePos.Y >= 0 && newTilePos.Y <= maxTilesY - 1)
                {
                    if (!exitTileSet && (newTilePos.X == 0 || newTilePos.X == maxTilesX - 1 || newTilePos.Y == 0 || newTilePos.Y == maxTilesY - 1) && playerTilePos.X != newTilePos.X && playerTilePos.Y != newTilePos.Y)
                    {
                        map[newTilePos.X, newTilePos.Y].SetToExit();
                        exitTileSet = true;
                        Console.WriteLine(newTilePos);
                    }
                    else if (newTilePos != playerTilePos)
                    {
                        // Delete the tile to create the path
                        generatedMap.Remove(map[newTilePos.X, newTilePos.Y]);


                        int spawnRng = RandomUtils.Next(50);
                        {
                            if (spawnRng == 0)
                            {
                                generatedMap.Add(new Yohann(map[newTilePos.X, newTilePos.Y].GetPos()));
                            }
                        }


                    }

                    pathTilePos = newTilePos;
                    //minSteps--;

                }

            }
            return generatedMap;

        }
    }
}
