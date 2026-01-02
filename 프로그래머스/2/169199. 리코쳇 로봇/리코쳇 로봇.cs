using System;
using System.Collections.Generic;

public class Solution
{
	int Height;
	int Width;
	bool[,] Visited;

	public int solution(string[] board)
	{
		int answer = -1;
		Height = board.Length;
		Width = board[0].Length;
		Visited = new bool[Height, Width];

		int rX = 0, rY = 0, gX = 0, gY = 0;
		for (int h = 0; h < Height; h++)
		for (int w = 0; w < Width; w++)
		{
			if (board[h][w] == 'R')
			{
				rY = h;
				rX = w;
			}
			else if (board[h][w] == 'G')
			{
				gY = h;
				gX = w;
			}
		}

		Queue<Vector> bfs = new Queue<Vector>();
		bfs.Enqueue(new Vector(rX, rY));
		bool solved = false;

		for (int tryCount = 0; bfs.Count != 0; tryCount++)
		{
			for (int bfsSize = bfs.Count; bfsSize > 0; bfsSize--)
			{
				Vector next = bfs.Dequeue();
				if (Visited[next.Y, next.X])
					continue;

				Visited[next.Y, next.X] = true;
				if (next.Y == gY && next.X == gX)
				{
					solved = true;
					break;
				}

				if (Push(board, next, Direction.Down, out Vector result))
				{
					bfs.Enqueue(result);
				}

				if (Push(board, next, Direction.Up, out Vector result2))
				{
					bfs.Enqueue(result2);
				}

				if (Push(board, next, Direction.Right, out Vector result3))
				{
					bfs.Enqueue(result3);
				}

				if (Push(board, next, Direction.Left, out Vector result4))
				{
					bfs.Enqueue(result4);
				}
			}

			if (solved)
			{
				answer = tryCount;
				break;
			}
		}

		return answer;
	}

	public class Vector
	{
		public int X;
		public int Y;

		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	public enum Direction
	{
		Up,
		Left,
		Right,
		Down
	}

	public bool Push(string[] board, Vector current, Direction d, out Vector result)
	{
		result = null;

		// Down
		if (d == Direction.Down)
		{
			if (current.Y == Height - 1) return false;

			int y = current.Y;
			for (; y < Height - 1; y++)
			{
				if (board[y + 1][current.X] == 'D') break;
			}

			result = new Vector(current.X, y);
			return true;
		}

		// Up
		if (d == Direction.Up)
		{
			if (current.Y == 0) return false;

			int y = current.Y;
			for (; y > 0; y--)
			{
				if (board[y - 1][current.X] == 'D') break;
			}

			result = new Vector(current.X, y);
			return true;
		}

		// Right
		if (d == Direction.Right)
		{
			if (current.X == Width - 1) return false;

			int x = current.X;
			for (; x < Width - 1; x++)
			{
				if (board[current.Y][x + 1] == 'D') break;
			}

			result = new Vector(x, current.Y);
			return true;
		}

		// Left
		if (d == Direction.Left)
		{
			if (current.X == 0) return false;

			int x = current.X;
			for (; x > 0; x--)
			{
				if (board[current.Y][x - 1] == 'D') break;
			}

			result = new Vector(x, current.Y);
			return true;
		}

		return false;
	}
}