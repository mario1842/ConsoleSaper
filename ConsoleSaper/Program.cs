namespace ConsoleSaper;

class Program
{
    
    static void Main(string[] args)
    {
        int width = 16;
        int height = 8;
        int bombs = 10;

        if (args.Length > 1)
        {
            int firstArg;
            int secondArg;
            bool isFirstNumeric = int.TryParse(args[0], out firstArg);
            bool isSecondNumeric = int.TryParse(args[1], out secondArg);
            if (isFirstNumeric && isSecondNumeric)
            {
                width = firstArg;
                height = secondArg;
            } else
            {
                Console.WriteLine("Arguments need to be a integer");
                return;
            }
            if (args.Length > 2)
            {
                int thirdArg;
                bool isThirdNumeric = int.TryParse(args[2], out thirdArg);
                if (isThirdNumeric)
                {
                    bombs = thirdArg;
                } else
                {
                    Console.WriteLine("Third argument need to be an integer");
                    return;
                }
            }
            
            
        }else if (args.Length == 1)
        {
            if (args[0] == "--help" || args[0] == "-h")
            {
                Console.WriteLine("Just a Saper in Terminal");
                Console.WriteLine();
                Console.WriteLine("Command Usage: ConsoleSaper width height bombs");
                Console.WriteLine("Example Usage:");
                Console.WriteLine("ConsoleSaper 16 16 12 or ConsoleSaper with no arguments to play default 8x8");
            }
        }
        
        SaperMap map = new SaperMap(width,height,bombs);
        map.Generate();
        map.TestShow();
    }
}