namespace ConsoleSaper;

public class GameHandler
{
    private bool _isRunning;
    private int _allFlagState;
    private int _flagState;  //correct flag placement
    
    private int[,] _map;
    private int[,] _visMap;
    
    private int _mapWidth;
    private int _mapHeight;
    
    private int _curWidth;
    private int _curHeight;
    
    public GameHandler(string title, int bombCount ,int[,] map)
    {
        Console.Title = title;
        this._map = map;
        
        this._flagState = bombCount;
        this._allFlagState = bombCount;
        this._curHeight = 0;
        this._curWidth = 0;
        
        this._mapHeight = map.GetLength(1);
        this._mapWidth = map.GetLength(0);
        
        this._visMap = new int[this._mapWidth,this._mapHeight];
    }

    public bool Start()
    {
        DrawMap();
        _isRunning = true;
        while (_isRunning)
        { 
            HandleInput();
            DrawMap();
            CheckIfWin();
        }
        return CheckIfWin();
    }

    private void DrawMap()
    {
        Console.Clear();
        for (int j = 0; j < this._mapHeight; j++)
        {
            for (int i = 0; i < this._mapWidth; i++)
            {
                if (this._curWidth == i && this._curHeight == j)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('[');
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(' ');
                }

                Console.ResetColor();

                switch (this._visMap[i, j])
                {
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("F");
                        Console.ResetColor();
                        break;
                    case 1:
                        if (this._map[i, j] == 9)
                        {
                            Console.Write("💣");
                        }
                        else
                        {
                            Console.ForegroundColor = GetNumberColor(this._map[i,j]);
                            Console.Write(this._map[i,j]);
                            Console.ResetColor();
                        }
                        break;
                    case 0:
                        Console.Write(' ');
                        break;
                }
                if (this._curWidth == i && this._curHeight == j)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(']');
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(' ');
                }

            }
            Console.WriteLine();
        }
    }

    private void HandleInput()
    {
        ConsoleKey key = Console.ReadKey(true).Key;
        switch (key)
        {
            case (ConsoleKey.UpArrow or ConsoleKey.W):
                this._curHeight +=  this._curHeight > 0 ? -1 : 0;
                break;
            case (ConsoleKey.DownArrow or ConsoleKey.S):
                this._curHeight +=  this._curHeight < this._mapHeight-1 ? 1 : 0;
                break;
            case (ConsoleKey.LeftArrow or ConsoleKey.A):
                this._curWidth +=  this._curWidth > 0 ? -1 : 0;
                break;
            case (ConsoleKey.RightArrow or ConsoleKey.D):
                this._curWidth +=  this._curWidth < this._mapWidth-1 ? 1 : 0;
                break; 
            case (ConsoleKey.Enter or ConsoleKey.Spacebar):
                SelectField(this._curWidth, this._curHeight);
                break;
            case (ConsoleKey.F or ConsoleKey.E):
                FlagField();
                break;
            case (ConsoleKey.Escape):
                this._isRunning = false;
                break;
        }
    }

    private void SelectField(int x, int y)
    {
        if (this._visMap[x,y] == 0)
        {
            this._visMap[x,y] = 1;
            if (this._map[x, y] == 9)
            {
                this._isRunning = false;
            }else if (this._map[x, y] == 0)
            {
                for (int yNumber = Math.Max(0, y - 1); yNumber <= Math.Min(y + 1, this._mapHeight - 1); yNumber++)
                {
                    for (int xNumber = Math.Max(0, x - 1); xNumber <= Math.Min(x + 1, this._mapWidth - 1); xNumber++)
                    {
                        SelectField(xNumber,yNumber);
                    }
                }
            }
        }
    }

    private void FlagField()
    {
        switch (this._visMap[this._curWidth, this._curHeight])
        {
            case 2: //jeśli oflagowane
                if (this._map[this._curWidth, this._curHeight] == 9)
                {
                    this._flagState++;
                }
                this._allFlagState++;
                this._visMap[this._curWidth, this._curHeight] = 0;
                break;
            case 0: //jeśli wolne
                if (this._map[this._curWidth, this._curHeight] == 9)
                {
                    this._flagState--;
                    
                }
                this._allFlagState--;
                this._visMap[this._curWidth, this._curHeight] = 2;
                break;
        }
    }

    private bool CheckIfWin()
    {
        if (this._flagState == 0 && this._allFlagState == 0)
        {
            this._isRunning = false;
            return true;
        }
        return false;
    }
    private ConsoleColor GetNumberColor(int number)
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