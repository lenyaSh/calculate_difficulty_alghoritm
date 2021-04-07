using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class Program {


        /// <summary>
        /// Случайная генерация матрицы
        /// </summary>
        /// <param name="matrix"></param>
        static void Generate(ref bool[,] matrix) {
            Random r = new Random();

            Console.WriteLine("Введите количество элементов:");
            int count_elem = int.Parse(Console.ReadLine());
            matrix = new bool[count_elem, count_elem];

            for(int i = 0; i < count_elem; i++) {
                for(int j = 0; j < count_elem; j++) {
                    if (i == j)
                        matrix[i, j] = true;
                    else
                        matrix[j, i] = matrix[i, j] = r.Next() % 2 == 0 ? false : true;
                }
            }

        }

        static void printData(bool[,] matrix) {
            Console.WriteLine($"Количество элементов = {matrix.GetLength(0)}\nМатрица смежности:");
            
            for (int i = 0; i < matrix.GetLength(0); i++) {
                Console.Write("| ");
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    int symb = matrix[i, j] == true ? 1 : 0;
                    Console.Write($"{symb} ");
                }
                Console.WriteLine("|\n");
            }

        }

        /// <summary>
        /// Читает булевую матрицу смежности из файла "input.txt"
        /// </summary>
        /// <param name="matrix">булевая матрица</param>
        static void ReadFile(ref bool[,] matrix) {
            using(StreamReader sr = new StreamReader("input.txt")) {
                int count_elem = int.Parse(sr.ReadLine());
                matrix = new bool[count_elem, count_elem];
                for(int i = 0; i < count_elem; i++) {
                    int j = 0;
                    foreach (string elem in sr.ReadLine().Split(' ').ToArray()) {
                        matrix[i, j] = elem == "1";
                        j++;
                    }
                }

            }
        }

        static void Main() {
            string answer = "y";
            while (answer == "y") {
                Console.WriteLine("======Задача о правильной раскраске графа======");
                Console.WriteLine("1 - сгенерировать входные данные\n2 - считать из файла input.txt");
                int selected = int.Parse(Console.ReadLine());
                bool[,] matrix = new bool[0, 0];

                // инициализация
                if (selected == 1) Generate(ref matrix);
                else ReadFile(ref matrix);

                Console.WriteLine("Нужно ли выводить матрицу? y/n");
                answer = Console.ReadLine();
                if (answer == "y") {
                    printData(matrix);
                }

                Console.WriteLine("======На выбор есть 3 алгоритма======");
                Console.Write($"1. Полный перебор\n2. Жадный алгоритм\n3. Жадный с оптимизацией\nВведите номер нужного алгоритма:");
                int number_alg = int.Parse(Console.ReadLine());

                if (number_alg == 1) {
                    TotallyOverkill subtask1 = new TotallyOverkill();
                    subtask1.Matrix = matrix;
                    object matr = subtask1.CalculateCountColors();
                }
                else if (number_alg == 2) {
                    GreedyAlg gr = new GreedyAlg(matrix.GetLength(0), matrix);
                    gr.GreedyColoring();
                }
                else if (number_alg == 3) {
                    OptGreedy ogr = new OptGreedy();
                    ogr.CalculateCountColors(matrix.GetLength(0), matrix);
                }

                Console.WriteLine("Желаете повторить? (y/n)");
                answer = Console.ReadLine();

                Console.Clear();
            }

        }
    }
}
