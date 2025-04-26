namespace TextRPG_Team25.Core
{
    public class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GameManager.Instance.Initialize();
            GameManager.Instance.Run();
        }
    }
}
