namespace ConsoleSaper;

public static class ConsoleRenderer
{
    public static void DrawGame(GameHandler game)
    {
        Console.Clear();
        for (int j = 0; j < game._mapHeight; j++)
        {
            for (int i = 0; i < game._mapWidth; i++)
            {
                if (game._curWidth == i && game._curHeight == j)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('[');
                    Console.ResetColor();
                }
                else if(game._visMap[i, j] == 0)
                {
                    Console.Write('[');
                }
                else
                {
                    Console.Write(' ');
                }

                Console.ResetColor();

                switch (game._visMap[i, j])
                {
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("F");
                        Console.ResetColor();
                        break;
                    case 1:
                        if (game._map[i, j] == 9)
                        {
                            Console.Write("💣");
                        }
                        else
                        {
                            Console.ForegroundColor = GetNumberColor(game._map[i,j]);
                            Console.Write(game._map[i,j]);
                            Console.ResetColor();
                        }
                        break;
                    case 0:
                        Console.Write(' ');
                        break;
                }
                if (game._curWidth == i && game._curHeight == j)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(']');
                    Console.ResetColor();
                }
                else if(game._visMap[i, j] == 0)
                {
                    Console.Write(']');
                }else
                {
                    Console.Write(' ');
                }

            }
            Console.WriteLine();
        }
    }
    private static ConsoleColor GetNumberColor(int number)
    {
        switch (number)
        {
            case 1:
                return  ConsoleColor.Blue;
                break;
            case 2:
                return  ConsoleColor.Green;
                break;
            case 3:
                return  ConsoleColor.Red;
                break;
            case 4:
                return  ConsoleColor.DarkBlue;
                break;
            case 5:
                return  ConsoleColor.DarkRed;
                break;
            case 6:
                return  ConsoleColor.DarkCyan;
                break;
            case 7:
                return  ConsoleColor.White;
                break;
            case 8:
                return ConsoleColor.DarkGray;
                break;
            case 9:
                return ConsoleColor.Black;
                break;
            default:
                return ConsoleColor.White;
                break;
        }
    }
}