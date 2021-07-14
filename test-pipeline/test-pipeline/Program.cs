using System;

namespace test_pipeline
{
    public class Program
    {
        static void Main(string[] args)
        {
            Triangulo triangulo = new Triangulo();
            Console.WriteLine(triangulo.CalcularArea(5, 8));
        }

        public class Triangulo
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public int CalcularArea(int Widht, int Height)
            {
                return ((Widht * Height) / 2);
            }
        }



    }
}
