class MyClass
{
    static int progress = 0;
    static CancellationTokenSource _tokenSource = null;
    static bool pauseFlag=false;
    public  static void Main(string[] args)
    {
        Task.WhenAll(Test());
        Console.ReadLine();

    }

    private static async Task Test()
    {
        _tokenSource = new CancellationTokenSource();
        var token = _tokenSource.Token;
        await Task.Run(() => LoopThroughNumbers(100, progress, token));
        await Task.Run(() => CancelMethod());
        Console.Write("CountDown Completed.");
        
    }

    private static void CancelMethod()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                pauseFlag= true;
                string action = Console.ReadLine();
                if (action.ToLower() == "cancel")
                {
                    Console.WriteLine("CountDown cancelled.");
                    _tokenSource.Cancel();
                    break;
                }
               
            }
        }
    }

    private static void LoopThroughNumbers(int count,int progress,CancellationToken token)
    {
       for(int i=0;i<count;i++)
        {
            if(!pauseFlag)
            {
                    Thread.Sleep(1000);
                    progress=(i*100)/count;
                    Console.WriteLine("Progress: "+progress);
                    if(token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
            }
            
        }
    }
} 