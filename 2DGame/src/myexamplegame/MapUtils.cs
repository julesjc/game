


using SFML.System;

namespace Game
{
    static class MapUtils
    {
        public static List<SceneSprite> GetGeneratedMap(Vector2f playerPos, bool spawnMobs)
        {
            Vector2f tileSize = new Vector2f(128, 128);

            Vector2u windowSize = AppManager.GetScreenSize();

            List<SceneSprite> generatedMap = new List<SceneSprite>();

            // number of tiles
            int maxTilesX = (int)Math.Ceiling(windowSize.X / tileSize.X);
            int maxTilesY = (int)Math.Ceiling(windowSize.Y / tileSize.Y);

            Tile[,] map = new Tile[maxTilesX, maxTilesY];

            // fill the map
            for (int x = 0; x < maxTilesX; x++)
            {
                for (int y = 0; y < maxTilesY; y++)
                {
                    map[x, y] = new Tile();

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

            // player tile
            Vector2i pathTilePos = playerTilePos;

            generatedMap.Remove(map[pathTilePos.X, pathTilePos.Y]);

            bool exitTileSet = false;

            // stop when tile to remove is out of the screen
            while (!exitTileSet)
            {
                // random direction : up/down/left/right
                int direction = RandomUtils.Next(4); // 0: up 1: down 2: left 3: right

                // new tile position based on random direction
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
                    }
                    else if (newTilePos != playerTilePos)
                    {
                        // create path
                        generatedMap.Remove(map[newTilePos.X, newTilePos.Y]);


                        if (spawnMobs)
                        {
                            int spawnRng = RandomUtils.Next(50);
                            {
                                if (spawnRng <= 5)
                                {
                                    generatedMap.Add(new EnemyFast(map[newTilePos.X, newTilePos.Y].GetPos()));
                                }

                                if (spawnRng >= 45)
                                {
                                    generatedMap.Add(new EnemyStatic(map[newTilePos.X, newTilePos.Y].GetPos()));
                                }
                            }
                        }


                    }

                    pathTilePos = newTilePos;

                }

            }
            return generatedMap;

        }
    }
}
