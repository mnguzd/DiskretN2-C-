using System;
using System.Collections.Generic;
namespace DiskretkaN2
{
    class Program
    {
        static Queue<int> WayDFS = new Queue<int>();
        static Queue<int> WayBFS = new Queue<int>();
        static int[][] SpisokSmezh = null;
        static bool[][] MarkedDFS = null;
        static bool[][] MarkedBFS = null;
        static int V = new int();
        static int select = new int();
        private static void SpisokSmezh_Input()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" Введите количество вершин: ");
            Console.ResetColor();
            V = Convert.ToInt32(Console.ReadLine());
            if (V == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Ошибка. Граф не может быть без вершин.");
                Console.ResetColor();
                Environment.Exit(0);
            }
            SpisokSmezh = null;
            GC.Collect();
            SpisokSmezh = new int[V][];
            Console.Clear();
            List<int>[] vs = new List<int>[V];
            for (int i = 0; i < V; i++)
                vs[i] = new List<int>();
            for (int i = 0; i < V; i++)
            {
                Console.Write(" Введите номера вершин, связанных с ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("x" + (i + 1) + ":\n");
                Console.ResetColor();
                for (int g = 0; g < V; g++)
                {
                    int selection = Convert.ToInt32(Console.ReadLine());
                    if (selection == 0)
                    {
                        vs[i].Add(0); break;
                    }
                    else
                        vs[i].Add(selection);
                }
            }
            for (int i = 0; i < V; i++)
            {
                SpisokSmezh[i] = vs[i].ToArray();
                vs[i].Clear();
            }
            MarkedDFS = null;WayDFS.Clear();WayDFS = new Queue<int>(); 
            WayBFS.Clear(); WayBFS = new Queue<int>(); MarkedBFS = null;
            GC.Collect();
            MarkedDFS = new bool[V][];
            for (int i = 0; i < V; i++)
                MarkedDFS[i] = new bool[SpisokSmezh[i].Length];
            for (int i = 0; i < V; i++)
                for (int g = 0; g < SpisokSmezh[i].Length; g++)
                    MarkedDFS[i][g] = false;
            MarkedBFS = new bool[V][];
            for (int i = 0; i < V; i++)
                MarkedBFS[i] = new bool[SpisokSmezh[i].Length];
            for (int i = 0; i < V; i++)
                for (int g = 0; g < SpisokSmezh[i].Length; g++)
                    MarkedBFS[i][g] = false;
            Spisok_Output();
        }
        private static void Choose_Searching()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\t Выберите вид обхода:\n\n "); Console.Write("\t1. ");
            Console.ResetColor();Console.Write("В глубину"); Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t2. "); Console.ResetColor(); Console.Write("В ширину");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t3. "); Console.ResetColor(); Console.Write("Ввести новый граф\n\n");
            Console.Write("Действие: ");
            byte selection = Convert.ToByte(Console.ReadLine());
            switch (selection)
            {
                case 1: Console.Clear(); Show_DFS(); break;
                case 2: Console.Clear(); Show_BFS(); break;
                case 3:  Console.Clear(); SpisokSmezh_Input(); break;
                default: Console.Clear(); SpisokSmezh_Input(); break;
            }
        }
        private static void Show_DFS()
        {
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("\n\tПуть обхода в глубину\n");
            Console.Write("\t" + WayDFS.Dequeue()); Console.ResetColor();
            while (WayDFS.Count != 0)
            {
                    Console.Write(" ---> "); Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(WayDFS.Dequeue()); Console.ResetColor();
            }
            Console.Write("\n\n Нажмите любую клавишу для выхода в меню выбора обхода: ");
            Console.ReadKey();
            Console.Clear(); Choose_Searching();
        }
        private static void Show_BFS()
        {
            Console.Clear();
            while (WayBFS.Count != 0)
            {
                Console.WriteLine(WayBFS.Dequeue());
            }
        }
        private static void DFS_Recurs(int V)
        {
            if (!WayDFS.Contains(V + 1)) 
                WayDFS.Enqueue(V + 1); 
            for (int i = 0; i < SpisokSmezh[V].Length; i++)
                if (SpisokSmezh[V][i] != 0 && !MarkedDFS[V][i])
                {
                    MarkedDFS[V][i] = true;
                    DFS_Recurs(SpisokSmezh[V][i] - 1);
                }
        }
        private static void Spisok_Output()
        {
            Console.Clear();Console.ForegroundColor = ConsoleColor.Blue; 
            Console.WriteLine("\n\t\tСписок смежности\n\n"); Console.ResetColor();
            for (int i = 0; i < V; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;Console.Write("\t\tx" + (i + 1));Console.ResetColor(); Console.Write(" - ");
                for (int g = 0; g < SpisokSmezh[i].Length; g++)
                {
                    if (g != 0)
                        Console.Write(",");
                    Console.Write(" " + SpisokSmezh[i][g]);
                }
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n Введи начальную вершину: ");
            Console.ResetColor();
            select = Convert.ToInt32(Console.ReadLine());
            DFS_Recurs(select - 1);
            for (int i = 0; i < V; i++)
                if (!WayDFS.Contains(i + 1))
                    DFS_Recurs(i);
            BFS(select - 1);
            Choose_Searching();
        }
        private static void BFS(int Vers)
        {
            Queue<int> temp = new Queue<int>();
            bool[] Marked = new bool[V];
            for (int i = 0; i < V; i++)
                Marked[i] = false;
            Marked[Vers] = true;
            temp.Enqueue(Vers);
            WayBFS.Enqueue(Vers+1);
            while (temp.Count != 0)
            {
                Vers = temp.Dequeue();
                for (int i = 0; i < SpisokSmezh[Vers].Length-1; i++)
                {
                    int jop = SpisokSmezh[Vers][i]-1;
                    if (!Marked[jop])
                    {
                        temp.Enqueue(jop);
                        WayBFS.Enqueue(jop+1);
                        Marked[jop] = true;
                    }

                }


            }
        }
        static void Main()
        {
            SpisokSmezh_Input();
        }
    }
}
