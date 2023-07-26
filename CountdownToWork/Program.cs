using System.Text;

namespace CountdownToWork
{
    // ■  □  ▋  █

    internal class Program
    {
        static int w;
        static int h;
        static ConsoleColor startColor;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:验证平台兼容性", Justification = "<挂起>")]
        static async Task Main(string[] args)
        {
            startColor = Console.ForegroundColor;

            Console.WindowWidth = 150;
            Console.WindowHeight = 40;

            w = Console.WindowWidth;
            h = Console.WindowHeight;

            var option = GetOption(args);
            var gulugulu = option.OffDutyTime;

            var now = DateTime.Now;
            var offDutyTime = new DateTime(now.Year, now.Month, now.Day, gulugulu.Hour, gulugulu.Minute, gulugulu.Second);

        start:
            Console.Clear();
            while (true)
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
                    goto start;
                }
                else if (number < 0 && number > -0.5)
                {
                    await ShowOffDutyGoGoGo();
                }
                else
                {
                    await ShowNumberText(number, option.Type);
                }
            }
        }

        static async Task ShowNumberText(double number, int type)
        {
            var numberArray = Number.DrawNumbuer(number.ToString("0.00000000"), Model.Init(type));
            // 输出的内容高是固定的 7
            // 宽度会随着字符的改变改变

            var widthReduction = type switch
            {
                1 => 92,
                2 => 82,
                3 => 62,
                4 => 92,
                5 => 82,
                6 => 85,
                _ => 56
            };

            var x = (Console.WindowWidth - widthReduction) / 2;
            x = x < 0 ? 0 : x;
            var y = (Console.WindowHeight - 7) / 2 - 1;

            StringBuilder stringBuilder = new();
            for (int i = 0; i < y; i++)
                stringBuilder.Append(Environment.NewLine);

            foreach (StringBuilder stringBuilder2 in numberArray)
                stringBuilder.Append(new string(' ', x)).Append(stringBuilder2).Append(Environment.NewLine);

            Console.WriteLine(stringBuilder.ToString());

            await Task.Delay(20);
        }

        static async Task ShowOffDutyGoGoGo()
        {
            Console.Clear();

            Random random = new Random();

            var gogogo =
                "   _____        _____        _____\r\n" +
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

                await Task.Delay(new Random().Next(40, 80));
            }

            Console.ForegroundColor = startColor;

            static ConsoleColor GetRandomConsoleColor()
            {
                return (ConsoleColor)new Random().Next((int)ConsoleColor.White);
            }
        }

        static Option GetOption(string[] args)
        {
            if (args.Length > 0)
            {
                var type = int.Parse(args.Length > 0 ? args[0] : "0");
                var gulugulu = DateTime.Parse(args.Length > 1 ? args[1] : "18:00:00");
                return new()
                {
                    Type = type,
                    OffDutyTime = gulugulu,
                };
            }
            else if (File.Exists("config.txt"))
            {
                var config = File.ReadAllText("config.txt");
                args = config.Split(" ");
                var type = int.Parse(args.Length > 0 ? args[0] : "0");
                var gulugulu = DateTime.Parse(args.Length > 1 ? args[1] : "18:00:00");
                return new()
                {
                    Type = type,
                    OffDutyTime = gulugulu,
                };
            }
            else
            {
                return new()
                {
                    Type = 0,
                    OffDutyTime = DateTime.Parse("18:00:00")
                };
            }
        }
    }

    public class Option
    {
        public int Type { get; set; }

        public DateTime OffDutyTime { get; set; }
    }

    public class Model
    {
        public static Model Init(int type)
        {
            switch (type)
            {
                case 1: return new Model("■", "  ", "  ", " ");
                case 2: return new Model("■", "□", "  ", " ");
                case 3: return new Model("▋", " ", " ", "  ");
                case 4: return new Model("▋", "  ", "  ", "  ");
                case 5: return new Model("█", @" ", " ", "  ");
                case 6: return new Model("█", @"  ", "  ", " ");
                default: return new Model("█", @" ", " ", "  ");
            }
        }

        private Model(string foreground = "█", string background = " ", string blank = " ", string interval = "  ")
        {
            Foreground = foreground;
            Background = background;
            Blank = blank;
            Interval = interval;
        }


        public string Foreground { get; set; }

        public string Background { get; set; }

        public string Blank { get; set; }

        public string Interval { get; set; }
    }

    public static class Number
    {
        private static readonly bool?[,] Zero = new bool?[7, 4] { { true, true, true, true }, { true, null, null, true }, { true, null, null, true }, { true, false, false, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] One = new bool?[7, 4] { { false, false, false, true }, { false, null, null, true }, { false, null, null, true }, { false, false, false, true }, { false, null, null, true }, { false, null, null, true }, { false, false, false, true } };
        private static readonly bool?[,] Two = new bool?[7, 4] { { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { true, true, true, true }, { true, null, null, false }, { true, null, null, false }, { true, true, true, true } };
        private static readonly bool?[,] Three = new bool?[7, 4] { { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] Four = new bool?[7, 4] { { true, false, false, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { false, false, false, true } };
        private static readonly bool?[,] Five = new bool?[7, 4] { { true, true, true, true }, { true, null, null, false }, { true, null, null, false }, { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] Six = new bool?[7, 4] { { true, true, true, true }, { true, null, null, false }, { true, null, null, false }, { true, true, true, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] Seven = new bool?[7, 4] { { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { false, false, false, true }, { false, null, null, true }, { false, null, null, true }, { false, false, false, true } };
        private static readonly bool?[,] Eight = new bool?[7, 4] { { true, true, true, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] Nine = new bool?[7, 4] { { true, true, true, true }, { true, null, null, true }, { true, null, null, true }, { true, true, true, true }, { false, null, null, true }, { false, null, null, true }, { true, true, true, true } };
        private static readonly bool?[,] Dot = new bool?[7, 1] { { false }, { false }, { false }, { false }, { false }, { false }, { true } };
        private static readonly bool?[,] MinusSign = new bool?[7, 2] { { false, false }, { false, false }, { false, false }, { true, true }, { false, false }, { false, false }, { false, false } };

        public static IEnumerable<bool?[,]> GetNumber(string numberText) => numberText.Select(GetNumbuer);

        public static bool?[,] GetNumbuer(char a)
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

        public static StringBuilder[] DrawNumbuer(string numberText, Model model)
        {
            var draw = new StringBuilder[7];
            foreach (var number in GetNumber(numberText))
            {
                var rows = 7;
                var columns = number.Length / rows;

                for (int row = 0; row < rows; row++)
                {
                    draw[row] ??= new();
                    for (int col = 0; col < columns; col++)
                    {
                        var pixel = number[row, col];
                        draw[row].Append(pixel is null ? model.Blank : pixel.Value ? model.Foreground : model.Background);
                    }
                    draw[row].Append(model.Interval);
                }
            }
            return draw;
        }
    }

}