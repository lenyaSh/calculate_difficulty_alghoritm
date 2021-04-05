using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class Program {
        static int Generate(ref bool[,] matrix) {
            Random r = new Random();
            int count_elem = r.Next(0, 10);
            matrix = new bool[count_elem, count_elem];

            for(int i = 0; i < count_elem; i++) {
                for(int j = 0; j < count_elem; j++) {
                    if (i == j)
                        matrix[i, j] = true;
                    else
                        matrix[j, i] = matrix[i, j] = r.Next() % 2 == 0 ? false : true;
                }
            }

            return count_elem;
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
        /// Reading boolean matrix from file "input.txt"
        /// </summary>
        /// <param name="matrix">boolean matrix</param>
        /// <returns>counting rows in matrix</returns>
        static int ReadFile(ref bool[,] matrix) {
            using(StreamReader sr = new StreamReader("input.txt")) {
                int count_elem = int.Parse(sr.ReadLine());
                matrix = new bool[count_elem, count_elem];
                for(int i = 0; i < count_elem; i++) {
                    int j = 0;
                    foreach (string elem in sr.ReadLine().Split(' ').ToArray()) {
                        matrix[i, j] = elem == "0";
                        j++;
                    }
                }

                return count_elem;
            }
        }

        static void Main() {
            Console.WriteLine("======Задача о правильной раскраске графа======");
            Console.WriteLine("1 - сгенерировать входные данные\n2 - считать из файла input.txt");
            int selected = int.Parse(Console.ReadLine());
            bool[,] matrix = new bool[0, 0];

            // инициализация
            if (selected == 1) Generate(ref matrix);
            else ReadFile(ref matrix);
            printData(matrix);


            TotallyOverkill subtask1 = new TotallyOverkill();
            subtask1.Matrix = matrix;
            object matr = subtask1.CalculateCountColors();


            Console.ReadLine();

        }
    }
}
