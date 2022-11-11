using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Написать приложение, которое будет отображать в консоли простейшие 
//геометрические фигуры заданные пользователем. Пользователь выбирает фигуру 
//и задает ее расположение на экране, а также размер и цвет с помощью меню. 
//Все заданные пользователем фигуры отображаются одновременно на экране. 
//Фигуры (прямоугольник, ромб, треугольник, трапеция, многоугольник) рисуются 
//звездочками или другими символами. Для реализации программы необходимо 
//разработать иерархию классов (продумать возможность абстрагирования). 
//Для хранения всех, заданных пользователем фигур, создать класс «Коллекция 
//геометрических фигур» с методом «Отобразить все фигуры». Чтобы отобразить 
//все фигуры указанным методом требуется использовать оператор foreach, 
//для чего в классе «Коллекция геометрических фигур» необходимо реализовать 
//соответствующие интерфейсы. 


namespace NET6_Task1
{
    class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point(double X, double Y)
        {
            x = X;
            y = Y;
        }
        public Point()
        {
            x = 0;
            y = 0;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }
        public Point(string str)
        {
            string[] strArr = str.Split(',');
            this.x = Convert.ToDouble(strArr[0]);
            this.y = Convert.ToDouble(strArr[1]);
        }
        public double distance(Point a)
        {
            double dist = 0;
            dist = Math.Sqrt(Math.Pow(x - a.x, 2) + Math.Pow(y - a.y, 2));
            return dist;
        }
        public void DrawLine(Point point, int step)
        {
            int Xside = (int)(this.x - point.x);
            int Yside = (int)(this.y - point.y);

            for (int i = 0; i < step; i++)
            {
                int Xpos = (int)(this.x - i * Xside/step);
                int Ypos = (int)(this.y - i * Yside/step);
                Console.SetCursorPosition(Xpos, Ypos);
                Console.Write('.');
            }
        }
    }

    interface IDrawable
    {
        void Draw(int NumberPoints);
    }
    public abstract class Geometric_Figure
    {
        public abstract double Figure_Square();
        public abstract double Figure_Perimeter();
        public abstract void Draw(int NumPoints);

    }
    class Triangle : Geometric_Figure

    {
        private Point p1;
        private Point p2;
        private Point p3;
        private ConsoleColor consoleColor;

        public Triangle(Point _p1, Point _p2, Point _p3, ConsoleColor _consoleColor)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _p3;
            consoleColor = _consoleColor;
        }

        public override double Figure_Square()
        {
            double figure_square = 0;
            double a = p1.distance(p2);
            double b = p2.distance(p3);
            double c = p3.distance(p1);
            double p = (a + b + c) / 2;

            figure_square = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            return figure_square;
        }
        public override double Figure_Perimeter()
        {
            double a = p1.distance(p2);
            double b = p2.distance(p3);
            double c = p3.distance(p1);

            return a + b + c;
        }

        public override string ToString()
        {
            string temp;
            Console.WriteLine("Это треугольник. Его точки:");
            temp = p1.ToString() + " " + p2.ToString() + " " + p3.ToString();
            Console.WriteLine(temp);
            return temp;
        }
        public override void Draw(int NumberPoints)
        {
            Console.ForegroundColor = consoleColor;

            p1.DrawLine(p2, NumberPoints);
            p2.DrawLine(p3, NumberPoints);
            p3.DrawLine(p1, NumberPoints);
            Console.ResetColor();
        }
    }
    class Diamond : Geometric_Figure
    {
        private Point p1;
        private Point p2;
        private Point p3;
        private Point p4;
        private Point center;
        private double radius1;
        private double radius2;
        private ConsoleColor consoleColor;
        public Diamond(Point _center, double _radius1, double _radius2, 
            ConsoleColor _consoleColor)
        {
            center = _center;
            radius1 = _radius1;
            radius2 = _radius2;
            p1 = new Point(center.x + radius1, center.y);
            p2 = new Point(center.x, center.y + radius2);
            p3 = new Point(center.x - radius1, center.y);
            p4 = new Point(center.x, center.y - radius2);
            consoleColor = _consoleColor;
        }

        public override double Figure_Square()
        {
            return radius1 * radius2 * 2;
        }
        public override double Figure_Perimeter()
        {
            return p1.distance(p2) * 4;
        }

        public override string ToString()
        {
            string temp;
            Console.WriteLine("Это ромб. Его точки:");
            temp = p1.ToString() + " " + p2.ToString() + " " + p3.ToString()
                + " " + p4.ToString() + $"\nСторона ромба равна {p1.distance(p2)}."
                + $"\nЦентр ромба: {center}"
                + $"\nПервый радиус ромба: {radius1}"
                + $"\nВторой радиус ромба: {radius2}";
            Console.WriteLine(temp);
            return temp;
        }
        public override void Draw(int NumberPoints)
        {
            Console.ForegroundColor = consoleColor;

            p1.DrawLine(p2, NumberPoints);
            p2.DrawLine(p3, NumberPoints);
            p3.DrawLine(p4, NumberPoints);
            p1.DrawLine(p4, NumberPoints);
            Console.ResetColor();

        }
    }
    class Rectangle : Geometric_Figure, IDrawable
    {
        private Point p1;
        private Point p2;
        private Point p3;
        private Point p4;
        private double side1;
        private double side2;
        private ConsoleColor consoleColor;
        public Rectangle(Point _p1, double _side1, double _side2, 
            ConsoleColor _consoleColor)
        {
            side1 = _side1;
            side2 = _side2;
            p1 = _p1;
            p2 = new Point(p1.x + side1, p1.y);
            p3 = new Point(p1.x + side1, p1.y + side2);
            p4 = new Point(p1.x, p2.y + side2);
            consoleColor = _consoleColor;
        }

        public override double Figure_Square()
        {
            return side1 * side2;
        }
        public override double Figure_Perimeter()
        {
            return 2 * (side1 + side2);
        }
        public override string ToString()
        {
            string temp;
            Console.WriteLine("Это прямоугольник. Его точки:");
            temp = p1.ToString() + " " + p2.ToString() + " " + p3.ToString()
                + " " + p4.ToString()
                + $"\nПервая сторона прямоугольника равна {side1}."
                + $"\nВторая сторона прямоугольника равна {side2}.";
            Console.WriteLine(temp);
            return temp;
        }
        public override void Draw(int NumberPoints)
        {
            Console.ForegroundColor = consoleColor;


            p1.DrawLine(p2, NumberPoints);
            p2.DrawLine(p3, NumberPoints);
            p3.DrawLine(p4, NumberPoints);
            p1.DrawLine(p4, NumberPoints);
            Console.ResetColor();
        }
    }
    class Trapezoid : Geometric_Figure, IDrawable
    {
        private Point p1;
        private Point p2;
        private Point p3;
        private Point p4;
        private double side_up;
        private double side_down;
        private double side;
        private double height;
        private ConsoleColor consoleColor;
        public Trapezoid(Point _p1, Point _p2, Point _p4, ConsoleColor _consoleColor)
        {
            p1 = _p1;
            p2 = _p2;
            p4 = _p4;
            height = Math.Abs(p1.y - p4.y);
            side = p1.distance(p4);
            side_down = p1.distance(p2);
            p3 = new Point(p2.x + (p1.x - p4.x), p4.y);
            side_up = p4.distance(p3);
            consoleColor = _consoleColor;
        }

        public override double Figure_Square()
        {
            return (side_up + side_down) * height / 2;
        }
        public override double Figure_Perimeter()
        {
            return 2 * side + side_up + side_down;
        }
        public override string ToString()
        {
            string temp;
            Console.WriteLine("Это трапеция. Ее точки:");
            temp = p1.ToString() + " " + p2.ToString() + " " + p3.ToString()
                + " " + p4.ToString()
                + $"\nНижняя сторона равна {side_up}."
                + $"\nВерхняя сторона равна {side_down}."
                + $"\nБоковая сторона равна {side}."
                + $"\nВысота трапеции {height}.";
            Console.WriteLine(temp);
            return temp;
        }
        public override void Draw(int NumberPoints)
        {
            Console.ForegroundColor = consoleColor;

            p1.DrawLine(p2, NumberPoints);
            p2.DrawLine(p3, NumberPoints);
            p3.DrawLine(p4, NumberPoints);
            p1.DrawLine(p4, NumberPoints);
            Console.ResetColor();
        }
    }
    class Polygon : Geometric_Figure, IDrawable

    {
        private Point[] point;
        int NumberOfVertex;
        private ConsoleColor consoleColor;

        public Polygon(Point[] _point, int _numbVertex, ConsoleColor _consoleColor)
        {
            NumberOfVertex = _numbVertex;
            point = _point;
            consoleColor = _consoleColor;
        }
        public override double Figure_Square() {
            return 0;
        }
        public override double Figure_Perimeter() 
        {
            return 0;
        }
        public override string ToString()
        {
            string temp = String.Empty;
            Console.WriteLine("Это многоугольник. Его точки:");
            foreach (Point p in point)
            {
                temp += p.ToString() + " ";
            }
            Console.WriteLine(temp);
            return temp;
        }
        public override void Draw(int NumberPoints)
        {
            Console.ForegroundColor = consoleColor;

            for (int i = 0; i < NumberOfVertex; i++)
            {
                if(i == NumberOfVertex-1)
                {
                    point[i].DrawLine(point[0], NumberPoints);
                }
                else
                {
                    point[i].DrawLine(point[i + 1], NumberPoints);
                }
            }

            Console.ResetColor();
        }
    }

    class CollectionOfGeometricFigures:IEnumerable
    {
        public Geometric_Figure[] Figures{ get; set; }
        public CollectionOfGeometricFigures(Geometric_Figure[] figures)
        {
            Figures = figures;
        }
        public CollectionOfGeometricFigures(int size)
        {
            Figures = new Geometric_Figure[size];
        }

        public void ShowAllFigures()
        {
            Console.Clear();
            foreach (Geometric_Figure f in Figures)
                f.Draw(50);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Figures.GetEnumerator();
        }
    }

    internal class NET6_Task1
    {
        static public void Menu()
        {       
            Console.WriteLine("1. Треугольник");
            Console.WriteLine("2. Ромб");
            Console.WriteLine("3. Прямоугольник");
            Console.WriteLine("4. Трапеция");
            Console.WriteLine("5. Многоугольник");
            Console.WriteLine("0. Завершить программу\n\n" +
                "Ваш выбор: ");
        }
        static void Main(string[] args)
        {
            Console.WindowHeight = 54;
            Console.WindowWidth = 150;
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
           
            int choice = 0;

            Console.WriteLine("Сколько фигур Вы хотите внести?\n" +
                "Ответ: ");
            int FigNumber = Convert.ToInt32(Console.ReadLine());
            CollectionOfGeometricFigures cogf = new CollectionOfGeometricFigures(FigNumber);

            for (int i =0; i < FigNumber; i++)
            {
                Console.Clear();
                Console.WriteLine($"Выберите фигуру {i+1}:");
                Menu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Вы выбрали треугольник.\n" +
                                "Задайте его параметры :");

                            Console.Write("Вершина 1 (разделитель - ','): ");
                            string TempStr = Console.ReadLine();
                            Point point1 = new Point(TempStr);

                            Console.Write("Вершина 2 (разделитель - ','): ");
                            TempStr = Console.ReadLine();
                            Point point2 = new Point(TempStr);

                            Console.Write("Вершина 3 (разделитель - ','): ");
                            TempStr = Console.ReadLine();
                            Point point3 = new Point(TempStr);

                            Console.WriteLine("Выберите цвет фигуры. Внесите число от 0 до 14: ");
                            int consoleColor = Convert.ToInt32(Console.ReadLine());

                            if (consoleColor >= 0 && consoleColor <= 14)
                            {
                                Triangle triangle = new Triangle(point1, point2, point3,
                                  colors[consoleColor]);
                                cogf.Figures[i] = triangle;
                            }
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Вы выбрали ромб.\n" +
                                   "Задайте его параметры: ");

                            Console.Write("Центр ромба (разделитель - ','): ");
                            string TempStr = Console.ReadLine();
                            Point point1 = new Point(TempStr);

                            Console.Write("Радиус1: ");
                            int radius1 = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Радиус2: ");
                            int radius2 = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Выберите цвет фигуры. Внесите число от 0 до 14: ");
                            int consoleColor = Convert.ToInt32(Console.ReadLine());

                            if (consoleColor >= 0 && consoleColor <= 14)
                            {
                                Diamond diamond = new Diamond(point1, radius1, radius2,
                                  colors[consoleColor]);
                                cogf.Figures[i] = diamond;
                            }
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Вы выбрали прямоугольник.\n" +
                                     "Задайте его параметры: ");

                            Console.Write("Вершина прямоугольника (разделитель - ','): ");
                            string TempStr = Console.ReadLine();
                            Point point1 = new Point(TempStr);

                            Console.Write("Сторона1: ");
                            int side1 = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Сторона2: ");
                            int side2 = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Выберите цвет фигуры. Внесите число от 0 до 14: ");
                            int consoleColor = Convert.ToInt32(Console.ReadLine());

                            if (consoleColor >= 0 && consoleColor <= 14)
                            {
                                Rectangle rectangle = new Rectangle(point1, side1, side2,
                                  colors[consoleColor]);
                                cogf.Figures[i] = rectangle;
                            }
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("Вы выбрали трапецию.\n" +
                                   "Задайте ее параметры: ");

                            Console.Write("Верхняя левая вершина (разделитель - ','): ");
                            string TempStr = Console.ReadLine();
                            Point leftUp = new Point(TempStr);

                            Console.Write("Верхняя правая вершина (разделитель - ','): ");
                            TempStr = Console.ReadLine();
                            Point rightUp = new Point(TempStr);

                            Console.Write("Нижняя левая вершина (разделитель - ','): ");
                            TempStr = Console.ReadLine();
                            Point leftDown = new Point(TempStr);

                            Console.WriteLine("Выберите цвет фигуры. Внесите число от 0 до 14: ");
                            int consoleColor = Convert.ToInt32(Console.ReadLine());

                            if (consoleColor >= 0 && consoleColor <= 14)
                            {
                                Trapezoid trapezoid = new Trapezoid(leftUp, rightUp, leftDown,
                                  colors[consoleColor]);
                                cogf.Figures[i] = trapezoid;
                            }
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Вы выбрали многоугольник.\n" +
                                    "Задайте его параметры: ");

                            Console.Write("Количество вершин: ");
                            int number = Convert.ToInt32(Console.ReadLine());

                            Point[] vertexes = new Point[number];

                            string TempStr = string.Empty;

                            for(int j = 0; j < number; j++)
                            {
                                Console.Write($"Вершина {j+1} (разделитель - ','): ");
                                TempStr = Console.ReadLine();
                                vertexes[j] = new Point(TempStr);               
                            }

                            Console.WriteLine("Выберите цвет фигуры. Внесите число от 0 до 14: ");
                            int consoleColor = Convert.ToInt32(Console.ReadLine());

                            if (consoleColor >= 0 && consoleColor <= 14)
                            {
                                Polygon polygon = new Polygon(vertexes, number,
                                  colors[consoleColor]);
                                cogf.Figures[i] = polygon;
                            }
                        }
                        break;
                    case 0:
                        return;
                    default:
                        {
                            Console.WriteLine("Неверный выбор. Повторите попытку.");
                        }
                        break;
                }
            }

            cogf.ShowAllFigures();
        }
    }
}


