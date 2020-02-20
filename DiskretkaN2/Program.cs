using System;
using System.Collections.Generic;
namespace DiskretkaN2
{
    class Program
    {
        static Queue<int> Way = new Queue<int>();
        static int[][] SpisokSmezh = null;
        static bool[][] Marked = null;
        static int V = new int();
        private static void SpisokSmezh_Input()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" Введите количество вершин: ");
            Console.ResetColor();
            try
            {
                V = Convert.ToInt32(Console.ReadLine());
                if (V == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Ошибка. Граф не может быть без вершин.");
                    Console.ResetColor();
                    Environment.Exit(0);
                }
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Ошибка. Введите число");
                Console.ResetColor();
                Environment.Exit(0);
            }
            SpisokSmezh = null;
            GC.Collect();
            SpisokSmezh = new int[V][];
            Console.Clear();
            List<int>[] vs = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                vs[i] = new List<int>();
            }
            try
            {
                int selection = new int();
                for (int i = 0; i < V; i++)
                {
                    Console.Write(" Введите номера вершин, связанных с ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("x" + (i + 1) + ":\n");
                    Console.ResetColor();
                    for (int g = 0; g < V; g++)
                    {
                        selection = Convert.ToInt32(Console.ReadLine());
                        if (selection == 0)
                        {
                            vs[i].Add(0);
                            break;
                        }
                        else
                        {
                            vs[i].Add(selection);
                        }
                    }

                }
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Ошибка. Список смежности введён в неправильном формате");
                Console.ResetColor();
                Environment.Exit(0);
            }
            for (int i = 0; i < V; i++)
            {
                SpisokSmezh[i] = vs[i].ToArray();
                vs[i].Clear();
            }
            GC.Collect();
        }
        private static void DFS_Recurs(int V)
        {
            if(!Way.Contains(V+1))
                Way.Enqueue(V+1);
            for (int i = 0; i < SpisokSmezh[V].Length; i++)
                if (SpisokSmezh[V][i]!=0)
                    if (!Marked[V][i])
                    {
                        Marked[V][i] = true;
                        DFS_Recurs(SpisokSmezh[V][i]-1);
                    }
        }
        private static void Spisok_Output()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\t\tСписок смежности\n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < V; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\tx" + (i + 1));
                Console.ResetColor(); Console.Write(" - ");
                for (int g = 0; g < SpisokSmezh[i].Length; g++)
                {
                    if (g != 0)
                    {
                        Console.Write(",");
                    }
                    Console.Write(" " + SpisokSmezh[i][g]);
                }
                Console.WriteLine("\n");
            }
        }
        static void Main()
        {
            SpisokSmezh_Input();
            Marked = new bool[V][];
            for (int i = 0; i < V; i++)
                Marked[i] = new bool[SpisokSmezh[i].Length];
            for (int i = 0; i < V; i++)
                for (int g = 0; g < SpisokSmezh[i].Length; g++)
                    Marked[i][g] = false;
            Spisok_Output();
            Console.WriteLine("VVedi vershinu");
            int select = Convert.ToInt32(Console.ReadLine());
            DFS_Recurs(select-1);
            for (int i = 0; i <V; i++)
                            if (!Way.Contains(i+1))
                                    DFS_Recurs(i);
            while (Way.Count != 0)
            {
                if (Way.Peek() == select)
                {
                    
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\tПуть обхода в глубину\n");
                    Console.Write("\t"+Way.Dequeue()); Console.ResetColor();
                }
                else
                {
                    Console.Write(" ---> "); Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(Way.Dequeue()); Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
}
