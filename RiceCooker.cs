using System;

class RiceCooker
{
    private bool isCooking = false;
    private bool isWarming = false;
    private System.Threading.Timer timer;

    private void StartCooking(int timeInMinutes)
    {
        if (isCooking)
        {
            Console.WriteLine("Rice cooker is already cooking. Wait for the current cycle to finish.");
            return;
        }

        Console.WriteLine($"Cooking rice for {timeInMinutes} minutes.");
        isCooking = true;

        timer = new System.Threading.Timer(_ =>
        {
            FinishCooking();
        }, null, timeInMinutes * 60 * 1000, System.Threading.Timeout.Infinite);
    }

    private void FinishCooking()
    {
        Console.WriteLine("Rice is cooked. Keep warm function activated.");
        isCooking = false;
        isWarming = true;
    }

    private void StopCooking()
    {
        if (isCooking)
        {
            timer.Dispose();
            Console.WriteLine("Cooking stopped. Rice may be undercooked.");
            isCooking = false;
        }
        else
        {
            Console.WriteLine("No cooking in progress.");
        }
    }

    private void StartWarming()
    {
        if (!isCooking)
        {
            Console.WriteLine("No cooking in progress. Unable to activate keep warm function.");
            return;
        }

        Console.WriteLine("Keep warm function activated.");
        isWarming = true;
    }

    private void StopWarming()
    {
        if (isWarming)
        {
            Console.WriteLine("Keep warm function deactivated.");
            isWarming = false;
        }
        else
        {
            Console.WriteLine("Keep warm function is not active.");
        }
    }

    public void RunCLI()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Start Cooking");
            Console.WriteLine("2. Stop Cooking");
            Console.WriteLine("3. Start Keep Warm");
            Console.WriteLine("4. Stop Keep Warm");
            Console.WriteLine("5. Turn off");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter cooking time in minutes: ");
                    int cookingTime = int.Parse(Console.ReadLine());
                    StartCooking(cookingTime);
                    break;
                case "2":
                    StopCooking();
                    break;
                case "3":
                    StartWarming();
                    break;
                case "4":
                    StopWarming();
                    break;
                case "5":
                    Console.WriteLine("Exiting rice cooker app.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        RiceCooker riceCooker = new RiceCooker();
        riceCooker.RunCLI();
    }
}
