namespace ConsoleSaper;

public class GameHandler
{
    private bool _isRunning;
    private int _allFlagState;
    private int _flagState;  //correct flag placement
    
    public int[,] _map;
    public int[,] _visMap;
    
    public int _mapWidth;
    public int _mapHeight;
    
    public int _curWidth;
    public int _curHeight;
    
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
        ConsoleRenderer.DrawGame(this);
        _isRunning = true;
        while (_isRunning)
        { 
            HandleInput();
            ConsoleRenderer.DrawGame(this);
            CheckIfWin();
        }
        return CheckIfWin();
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
    

}