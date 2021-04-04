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

        static void printData(int count_elem, bool[,] matrix) {
            Console.WriteLine($"Количество элементов = {count_elem}\nМатрица смежности:");
            
            for (int i = 0; i < count_elem; i++) {
                Console.Write("| ");
                for (int j = 0; j < count_elem; j++) {
                    int symb = matrix[i, j] == true ? 1 : 0;
                    Console.Write($"{symb} ");
                }
                Console.WriteLine("|\n");
            }

        }

        static int ReadFile(ref bool[,] matrix) {
            using(StreamReader sr = new StreamReader("input.txt")) {
                int count_elem = int.Parse(sr.ReadLine());
                matrix = new bool[count_elem, count_elem];
                for(int i = 0; i < count_elem; i++) {
                    for(int j = 0; j < count_elem; j++) {
                        char x = (char)sr.Read();
                        if (x == (char)32) {
                            x = (char)sr.Read();
                        }

                        matrix[i, j] = x == '0' ? false : true;
                    }
                }

                return count_elem;
            }
        }

        static void Main() {
            Console.WriteLine("======Задача о правильной раскраске графа======");
            Console.WriteLine("1 - сгенерировать входные данные\n2 - считать из файла input.txt");
            int selected = int.Parse(Console.ReadLine());
            int count_elem = 0;
            bool[,] matrix = new bool[0, 0];

            // инициализация
            count_elem = selected == 1 ? Generate(ref matrix) : ReadFile(ref matrix);
            printData(count_elem, matrix);

            Console.ReadLine();

        }
    }
}
