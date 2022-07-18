// See https://aka.ms/new-console-template for more information

namespace IOET
{
    public class Program {
        public static void Main()
        {
            Console.Write("URL: ");
            var file = Console.ReadLine() ?? string.Empty;
            if (file == string.Empty)
            {
                Console.WriteLine("Add a valid URL file");
                Console.ReadLine();
            }
            file = Console.ReadLine() ?? string.Empty;

            ProcessService processService = new();
            processService.ProcessData(file);
        }
    }
}