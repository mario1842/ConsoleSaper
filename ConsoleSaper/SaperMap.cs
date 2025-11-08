namespace ConsoleSaper;

public class SaperMap
{
    private int _x;
    private int _y;
    private int _bombCount;

    private int[,] _map;

    public SaperMap(int x, int y, int bombs)
    {
        this._x = x;
        this._y = y;
        this._bombCount = bombs;
        this._map = new int[this._x, this._y];
    }
    
    public int[,] Generate()
    {
        GenerateBombs();
        GenerateNumbers();
        
       return this._map;
    }

    private void GenerateBombs()
    {
        Random random = new Random();
        int bombToPlace = _bombCount;
        while(bombToPlace > 0)
        {
            int xrand = random.Next(0, this._x - 1);
            int yrand = random.Next(0, this._y - 1);

            if (this._map[xrand, yrand] != 9)
            {
                this._map[xrand, yrand] = 9; // 9 to bomba
                bombToPlace--;
            }

        }
    }
    private void GenerateNumbers()
    {
        for (int j = 0; j < this._y; j++) 
        {
            for (int i = 0; i < this._x; i++)
            {
                if (!CheckIfBomb(i,j))
                {
                    for (int yNumber = Math.Max(0, j - 1); yNumber <= Math.Min(j + 1, this._y - 1); yNumber++)
                    {
                        for (int xNumber = Math.Max(0, i - 1); xNumber <= Math.Min(i + 1, this._x - 1); xNumber++)
                        {
                            this._map[i, j] += CheckIfBomb(xNumber, yNumber) ?  1 : 0;
                        }
                    }
                    
                }
                
            }
        }


    }

    private bool CheckIfBomb(int xpos, int ypos)
    {
        if (_map[xpos, ypos] == 9)
        {
            return true;
        }
        return false;
    }

}


