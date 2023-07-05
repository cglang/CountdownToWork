using System.Text;

namespace CountdownToWork
{
    internal class Program
    {
        static int w;
        static int h;
        static ConsoleColor startColor;

        static void Main(string[] args)
        {
            startColor = Console.ForegroundColor;
            w = Console.WindowWidth;
            h = Console.WindowHeight;

            var now = DateTime.Now;
            DateTime gulugulu = DateTime.Parse(args.Length > 0 ? args[0] : "18:00:00");
            var offDutyTime = new DateTime(now.Year, now.Month, now.Day, gulugulu.Hour, gulugulu.Minute, gulugulu.Second);

            while (true)
            {
                try
                {
                    if (w != Console.WindowWidth || h != Console.WindowHeight)
                    {
                        Console.Clear();
                        w = Console.WindowWidth;
                        h = Console.WindowHeight;
                    }

                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, 0);

                    var number = Math.Round((offDutyTime - DateTime.Now).TotalHours, 8);
                    if (number < -6)
                    {
                        offDutyTime = offDutyTime.AddDays(1);
                        break;
                    }

                    if (number < 0 && number > -0.5)
                    {
                        OffDuty();
                    }
                    else
                    {
                        StringBuilder[] array = WaitCallback(number.ToString());

                        // 输出的内容宽高是固定的 56*7
                        var x = (Console.WindowWidth - 56) / 2;
                        var y = (Console.WindowHeight - 7) / 2 - 1;

                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < y; i++)
                            stringBuilder.Append(Environment.NewLine);
                        StringBuilder[] array2 = array;
                        foreach (StringBuilder stringBuilder2 in array2)
                        {
                            stringBuilder.Append(new string(' ', x)).Append(stringBuilder2).Append(Environment.NewLine);
                        }
                        Console.WriteLine(stringBuilder.ToString());
                    }
                }
                catch { }
                Thread.Sleep(new Random().Next(90, 100));
            }
        }

        static void OffDuty()
        {
            Console.Clear();

            Random random = new Random();

            var gogogo = "   _____        _____        _____\r\n" +
                "  / ____|      / ____|      / ____|      \r\n" +
                " | |  __  ___ | |  __  ___ | |  __  ___  \r\n" +
                " | | |_ |/ _ \\| | |_ |/ _ \\| | |_ |/ _ \\ \r\n" +
                " | |__| | (_) | |__| | (_) | |__| | (_) |\r\n" +
                "  \\_____|\\___/ \\_____|\\___/ \\_____|\\___/ ";

            // 输出的内容宽高是固定的 56*7
            var x1 = (Console.WindowWidth - 40) / 2;
            var y1 = (Console.WindowHeight - 6) / 2 - 1;

            for (int i = 0; i < y1; i++)
                Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            var gos = gogogo.Split("\r\n");
            foreach (var g in gos)
            {
                Console.Write(new string(' ', x1));
                Console.WriteLine(g);
            }

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Console.WindowWidth);
                int y = random.Next(Console.WindowHeight);

                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = GetRandomConsoleColor();
                Console.Write("*");

                Thread.Sleep(new Random().Next(40, 80));
            }

            Console.ForegroundColor = startColor;

            static ConsoleColor GetRandomConsoleColor()
            {
                return (ConsoleColor)new Random().Next((int)ConsoleColor.White);
            }
        }

        static StringBuilder[] WaitCallback(string t)
        {
            StringBuilder[] array3 = new StringBuilder[7];
            foreach (bool[,] item in Number.GetNumbuer(t))
            {
                int length = item.GetLength(0);
                int length2 = item.GetLength(1);
                for (int k = 0; k < length; k++)
                {
                    ref StringBuilder reference = ref array3[k];
                    if (reference == null)
                    {
                        reference = new StringBuilder();
                    }
                    for (int l = 0; l < length2; l++)
                    {
                        string text = (item[k, l] ? "█" : " ");
                        array3[k].Append(text);
                    }
                    array3[k].Append("  ");
                }
            }
            return array3;
        }
    }

    public static class Number
    {
        public static bool[,] Zero = new bool[7, 4]
        {
        { true, true, true, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] One = new bool[7, 4]
        {
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true }
        };

        public static bool[,] Two = new bool[7, 4]
        {
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { true, true, true, true },
        { true, false, false, false },
        { true, false, false, false },
        { true, true, true, true }
        };

        public static bool[,] Three = new bool[7, 4]
        {
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] Four = new bool[7, 4]
        {
        { true, false, false, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true }
        };

        public static bool[,] Five = new bool[7, 4]
        {
        { true, true, true, true },
        { true, false, false, false },
        { true, false, false, false },
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] Six = new bool[7, 4]
        {
        { true, true, true, true },
        { true, false, false, false },
        { true, false, false, false },
        { true, true, true, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] Seven = new bool[7, 4]
        {
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true },
        { false, false, false, true }
        };

        public static bool[,] Eight = new bool[7, 4]
        {
        { true, true, true, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] Nine = new bool[7, 4]
        {
        { true, true, true, true },
        { true, false, false, true },
        { true, false, false, true },
        { true, true, true, true },
        { false, false, false, true },
        { false, false, false, true },
        { true, true, true, true }
        };

        public static bool[,] Dot = new bool[7, 1]
        {
        { false },
        { false },
        { false },
        { false },
        { false },
        { false },
        { true }
        };

        public static bool[,] MinusSign = new bool[7, 2]
        {
        { false, false },
        { false, false },
        { false, false },
        { true, true },
        { false, false },
        { false, false },
        { false, false }
        };

        public static List<bool[,]> GetNumbuer(string a)
        {
            List<bool[,]> list = new List<bool[,]>();
            foreach (char a2 in a)
            {
                list.Add(GetNumbuer(a2));
            }
            return list;
        }

        public static bool[,] GetNumbuer(char a)
        {
            return a switch
            {
                '0' => Zero,
                '1' => One,
                '2' => Two,
                '3' => Three,
                '4' => Four,
                '5' => Five,
                '6' => Six,
                '7' => Seven,
                '8' => Eight,
                '9' => Nine,
                '.' => Dot,
                '-' => MinusSign,
                _ => throw new NotImplementedException(),
            };
        }
    }

}