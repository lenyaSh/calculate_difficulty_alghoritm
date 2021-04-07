using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class TotallyOverkill {
        public bool[,] Matrix { get; set; }
        

        /// <summary>
        /// Считает минимальное кол-во цветов в графе
        /// </summary>
        /// <returns>пара номер вершины - номер цветаr</returns>
        public Dictionary<int, int> CalculateCountColors() {
            if (Matrix == null)
                return null;

            bool[] numbers = new bool[Matrix.GetLength(0)];
            
            Dictionary<int, int> colorsAndVertex = new Dictionary<int, int> {
                { 0, 1 }
            };

            for (int i = 1; i < Matrix.GetLength(0); i++) {
                for(int j = 0; j < Matrix.GetLength(1); j++) {

                    if (Matrix[i,j] && i != j && colorsAndVertex.ContainsKey(j)) {
                        numbers[colorsAndVertex[j] - 1] = true; 
                    } 

                }
                for(int j = 0; j < Matrix.GetLength(0); j++) {
                    if (!numbers[j]) {
                        colorsAndVertex.Add(i, j + 1);
                        break;
                    }
                }
                numbers = new bool[numbers.Length];
            }


            print(colorsAndVertex);

            return colorsAndVertex;
        }

        /// <summary>
        /// Форматированный вывод
        /// </summary>
        /// <param name="c">пара номер вершины - номер цвета</param>
        void print(Dictionary<int,int> c) {
            int max = 0;

            Console.WriteLine("Нужно ли выводить список вершина - цвет? y/n");
            string answer = Console.ReadLine();

            if (answer == "y")
                Console.WriteLine("Номер вершины - номер цвета:");
            foreach (KeyValuePair<int, int> elem in c) {
                if (elem.Value > max) max = elem.Value;
                if (answer == "y")
                    Console.WriteLine($"{elem.Key + 1} - {elem.Value}");
            }
            Console.WriteLine("Кол-во цветов: ");
            Console.WriteLine(max);
        }
    }
}
