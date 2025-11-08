namespace ConsoleSaper;

public class SaperMap
{
    private int x;
    private int y;
    private int bombCount;

    private int[,] map;

    public SaperMap(int x, int y, int bombs)
    {
        this.x = x;
        this.y = y;
        this.bombCount = bombs;
        this.map = new int[this.x, this.y];
    }
    
    public int[,] Generate()
    {
        GenerateBombs();
        GenerateNumbers();
        
       return this.map;
    }

    private void GenerateBombs()
    {
        Random random = new Random();
        int bombToPlace = bombCount;
        while(bombToPlace > 0)
        {
            int xrand = random.Next(0, this.x - 1);
            int yrand = random.Next(0, this.y - 1);

            if (this.map[xrand, yrand] != 9)
            {
                this.map[xrand, yrand] = 9; // 9 to bomba
                bombToPlace--;
            }

        }
    }
    private void GenerateNumbers()
    {
        for (int j = 0; j < this.y; j++) 
        {
            for (int i = 0; i < this.x; i++)
            {
                if (!CheckIfBomb(i,j))
                {

                    for (int yNumber = Math.Max(0, j - 1); yNumber <= Math.Min(j + 1, this.y - 1); yNumber++)
                    {
                        for (int xNumber = Math.Max(0, i - 1); xNumber <= Math.Min(i + 1, this.x - 1); xNumber++)
                        {
                            this.map[i, j] += CheckIfBomb(xNumber, yNumber) ?  1 : 0;
                        }
                    }
                    
                }
                
            }
        }


    }

    private bool CheckIfBomb(int xpos, int ypos)
    {
        if (map[xpos, ypos] == 9)
        {
            return true;
        }
        return false;
    }

    public void TestShow()
    {
        Console.Clear();
        for (int j = 0; j < this.y; j++) 
        {
            for (int i = 0; i < this.x; i++)
            {
                int numerek = this.map[i, j];

                switch (numerek)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                }

                Console.Write(this.map[i, j]+ " ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

}


