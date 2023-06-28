using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11
{
    public class Generator
    {
        char[,] Maze = new char[0,0];

        /// <summary>
        /// Генерирует карту лабиринта. 
        /// (Размер лабиринта, Максимальная ширина, Наличие прямых галлерей, Наличие квадратов из максимальной ширины)
        /// </summary>
        /// <returns></returns>
        public char[,] Generate(int Size, int MaxWidth, bool Galery, bool Squares)
        {
            Maze = new char[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                    Maze[i, j] = '|';
            }

            Random rnd = new((int)DateTime.Now.Ticks);
            int[] stPoint = new int[2] { 0, rnd.Next(Size) };
            Maze[stPoint[0], stPoint[1]] = 'S';

            int[][] directions =
            {
                new [] { 0, 1 },
                new [] { 0, -1 },
                new [] { 1, 0 },
                new [] { -1, 0 }
            };
            Stack<int[]> stack = new();
            stack.Push(new int[2] { stPoint[0], stPoint[1] });

            while (stack.Count > 0)
            {
                int[] cPoint = stack.First();
                List<int[]> neighbors = new();

                foreach (int[] direct in directions)
                {
                    int[] nPoint = new int[cPoint.Length];
                    nPoint[0] = cPoint[0] + direct[0] * 2;
                    nPoint[1] = cPoint[1] + direct[1] * 2;

                    if (0 < nPoint[0] && nPoint[0] < Size-1 && 0 < nPoint[1] && nPoint[1] < Size - 1 && Maze[nPoint[0], nPoint[1]] == '|')
                        neighbors.Add(nPoint);
                }

                if (neighbors.Count > 0)
                {
                    int[] nPoint = neighbors[rnd.Next(neighbors.Count)];
                    Maze[nPoint[0], nPoint[1]] = ' ';
                    Maze[(cPoint[0] + (nPoint[0] - cPoint[0]) / 2), (cPoint[1] + (nPoint[1] - cPoint[1]) / 2)] = ' ';
                    stack.Push(nPoint);
                }
                else stack.Pop();
            }

            int[][] lPoints =
            {
                new [] { 0, rnd.Next(1, Size - 1) },
                new [] { Size - 1, rnd.Next(1, Size - 1) },
                new [] { rnd.Next(1, Size - 1), 0 },
                new [] { rnd.Next(1, Size - 1), Size - 1},
            };
            int[] lPoint = lPoints[rnd.Next(4)];

            Maze[lPoint[0], lPoint[1]] = 'F';
            if (lPoint[0] == 0)
            {
                Maze[lPoint[0] + 1, lPoint[1]] = ' ';
                Maze[lPoint[0] + 2, lPoint[1]] = ' ';
            }
            else if (lPoint[0] == Maze.GetLength(0) - 1)
            {
                Maze[lPoint[0] - 1, lPoint[1]] = ' ';
                Maze[lPoint[0] - 2, lPoint[1]] = ' ';
            }
            else if (lPoint[1] == 0)
            {
                Maze[lPoint[0], lPoint[1] + 1] = ' ';
                Maze[lPoint[0], lPoint[1] + 2] = ' ';
            }
            else if (lPoint[1] == Maze.GetLength(1) - 1)
            {
                Maze[lPoint[0], lPoint[1] - 1] = ' ';
                Maze[lPoint[0], lPoint[1] - 2] = ' ';
            }

            Raze(ref Maze, MaxWidth, Galery, Squares);
            FindWay(ref Maze);
            return Maze;
        }

        private static void Raze(ref char[,] Maze, int MaxWidth, bool Galery, bool Squares)
        {
            //MaxWidth -= 2;
            Random rnd = new((int)DateTime.Now.Ticks);

            if (Squares)
            {
                for (int c = Math.Min(Maze.GetLength(0) / (MaxWidth * 3), Maze.GetLength(1) / (MaxWidth * 3)); c > 0; c--)
                {
                Proeb:

                    int[] sPoint = new int[2];
                    sPoint[0] = rnd.Next(1, Maze.GetLength(0) - MaxWidth - 2);
                    sPoint[1] = rnd.Next(1, Maze.GetLength(1) - MaxWidth - 2);
                    int[] fPoint = new int[2];
                    fPoint[0] = sPoint[0] + MaxWidth + 2;
                    fPoint[1] = sPoint[1] + MaxWidth + 2;

                    Stack<int[]> spaces = new(MaxWidth ^ 2);
                    List<int[]> walls = new((MaxWidth + 2) * 4);

                    for (int i = sPoint[0]; i < fPoint[0]; i++)
                    {
                        for (int j = sPoint[1]; j < fPoint[1]; j++)
                        {
                            if (Maze[i, j] == 'S')
                            {
                                goto Proeb; }

                            if (i == sPoint[0] || j == sPoint[1] || i == fPoint[0]-1 || j == fPoint[1]-1)
                                walls.Add(new int[] { i, j });
                            else spaces.Push(new int[] { i, j });
                        }
                    }

                    foreach (int[] space in spaces)
                        Maze[space[0], space[1]] = ' ';
                }
            }

            if (Galery)
            {
                short countI = 0;
                for (int i = MaxWidth; i < Maze.GetLength(0) - MaxWidth; i++)
                {
                    for (int j = MaxWidth; j < Maze.GetLength(1) - MaxWidth; j++)
                    {
                        if (Maze[i - 1, j] == ' ' && Maze[i, j] == '|' && Maze[i + 1, j] == ' ') countI++;
                        else countI = 0;

                        if (countI == MaxWidth)
                            for (int k = j - MaxWidth; k <= j; k++)
                                Maze[i, k] = ' ';
                    }
                }
            }
        }


        public void FindWay(ref char[,] maze)
        {
            if (Maze == null) return;

            int Size = maze.GetLength(0);
            bool[,] visited = new bool[Size, Size]; // Матрица для отслеживания посещенных клеток
            (int, int)[] directions = { (0, 1), (0, -1), (1, 0), (-1, 0) }; // Возможные направления движения: вправо, влево, вниз, вверх

            // Находим стартовую позицию
            int stRow = -1;
            int stCol = -1;
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (maze[row,col] == 'S')
                    {
                        stRow = row;
                        stCol = col;
                        break;
                    }
                }
                if (stRow != -1) break;
            }

            Queue<(int, int, List<(int, int)>)> queue = new(); // очередь для обхода клеток
            queue.Enqueue((stRow, stCol, new List<(int, int)>()));
            visited[stRow, stCol] = true; // Помечаем стартовую позицию как посещенную

            while (queue.Count > 0)
            {
                (int current_row, int current_col, List<(int, int)> path) = queue.Dequeue();

                if (maze[current_row, current_col] == 'F') // Если текущая клетка - выход
                {
                    for (int i = 0; i < path.Count-1; i++)
                        maze[path[i].Item1, path[i].Item2] = '*';
                }

                foreach ((int, int) direction in directions)
                {
                    int next_row = current_row + direction.Item1;
                    int next_col = current_col + direction.Item2;

                    // Проверяем, можно ли перейти в следующую клетку и она не была посещена ранее
                    if (0 <= next_row && next_row < Size && 0 <= next_col && next_col < Size && maze[next_row, next_col] != '|' && !visited[next_row, next_col])
                    {
                        visited[next_row, next_col] = true; // Помечаем клетку как посещенную
                        List<(int, int)> new_path = new(path)
                        {
                            (next_row, next_col)
                        };
                        queue.Enqueue((next_row, next_col, new_path)); // Добавляем клетку в очередь с обновленным путем
                    }
                }
            }
        }

    }
}
