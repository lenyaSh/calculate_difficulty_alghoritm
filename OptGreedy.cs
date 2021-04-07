using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class OptGreedy {
        public void CalculateCountColors(int count_elem, bool[,] matrix) {
            int pow;
            int[] table_pow = new int[count_elem];

            for (int i = 0; i < count_elem; i++) {
                pow = 0;
                for (int j = 0; j < count_elem; j++) {
                    if (matrix[i, j] == true) {
                        pow++;
                    }
                }
                table_pow[i] = pow - 1;
            }


            int[] table_color = new int[count_elem]; //таблица цветов вершин
            List<List<int>> table_forbidden_colors = new List<List<int>>(); //список списков, в котором буде хранить знчение запрещенных цветов для каждой вершины
            int color = 1; // первоначальное значение цвета
            int max;//переменная для хранения макс.степени вершины
            bool change = false;

            for (int i = 0; i < count_elem; i++) //инициализация списка запрещенных цветов
            {
                table_forbidden_colors.Add(new List<int>());
            }
            int rep = 0;

            while (rep < count_elem) {
                max = table_pow.Max(); //поиск максимального значения в таблице степеней
                int index_max = Array.IndexOf(table_pow, max); //поиск индекса максимального значения для поиск смежных вершин


                for (int j = 0; j < table_forbidden_colors[index_max].Count; j++) //пробешаемся по списку запрещенных цветов для вершины
                {
                    if (table_forbidden_colors[index_max][j] == color) //если цвет запрещен, то меняем цвет
                    {
                        color++;
                        table_color[index_max] = color;
                        change = true;
                    }
                }
                if (change == false) {
                    table_color[index_max] = color;
                }
                change = false;
                for (int i = 0; i < count_elem; i++) // поиск смежных с максимальной по  степени вершины с другими вершинами в таблице смежности. Если смежны,то в таблице степеней вычитаем -1 у смежных до значения 0. значение -1 показывает, что это таже самая вершина. 
                {
                    if (matrix[index_max, i] == true) //есил смежны, то
                    {
                        if (index_max == i) // если индекс. максимального совпадает с текущим, то присвоить этому индекс -1, чтобы в дальнейшем больше не просматривать его
                        {
                            table_pow[i] = -1;
                        }
                        else {
                            if (table_pow[i] == -1)// если символ -1, то оставляем -1
                            {
                                table_pow[i] = -1;
                            }
                            else {
                                table_forbidden_colors[i].Add(color);
                                if (table_pow[i] > 0) //если значение в таблице степеней больше 0, то -1
                                {
                                    table_pow[i]--;
                                }
                                else table_pow[i] = 0; //иначе 0. 
                            }
                        }
                    }
                }

                rep++;
            }


            Console.WriteLine("Минимальное количество цветов, необходимое для раскраски графа: " + table_color.Max());
            Console.WriteLine("Нужно ли выводить список вершина - цвет? y/n");
            string answer = Console.ReadLine();

            if (answer == "y") {
                Console.WriteLine("\n" + "Таблица цветов для вершин:");
                for (int i = 0; i < count_elem; i++) {
                    Console.Write("Вершина № " + (i + 1) + " - ");
                    if (table_color[i] == 0) {
                        table_color[i] = table_color.Max();
                    }
                    Console.Write(table_color[i] + "\n");
                }
            }
        }

    }
}
