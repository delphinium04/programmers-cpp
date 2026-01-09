using System;
using System.Collections.Generic;
using System.Linq;

public struct Point
{
	public int Y;
	public int X;
}

public class Solution
{
	public class Robot
	{
		public Queue<Point> Route;
		public Point Current;

		public Robot(Queue<Point> route)
		{
			Route = route;
		}

		public void Act()
		{
			Current = Route.Dequeue();
		}

		public bool Done()
		{
			return Route.Count == 0;
		}
	}

	public Queue<Point> GetRoute(params Point[] points)
	{
		// 직선 루트가 가능한가?
		Queue<Point> route = new Queue<Point>();

		for (int i = 1; i < points.Length; i++)
		{
			Point from = points[i - 1];
			Point to = points[i];
			int currentX = from.X;
			int currentY = from.Y;

			while (currentY != to.Y)
			{
				route.Enqueue(new Point { X = currentX, Y = currentY });
				currentY += from.Y < to.Y ? 1 : -1;
			}

			while (currentX != to.X)
			{
				route.Enqueue(new Point { X = currentX, Y = currentY });
				currentX += from.X < to.X ? 1 : -1;
			}
		}

		route.Enqueue(points[points.Length - 1]);
		return route;
	}

	public int solution(int[,] points, int[,] routes)
	{
		// 디버그 편의용 field 범위 제한
		int Width = -1;
		int Height = -1;
		for (int p = 0; p < points.GetLength(0); p++)
		{
			Width = Math.Max(Width, points[p, 0] + 1);
			Height = Math.Max(Height, points[p, 1] + 1);
		}

		List<Robot> robots = new List<Robot>();

		int answer = 0;
		for (int r = 0; r < routes.GetLength(0); r++)
		{
			Point[] spots = new Point[routes.GetLength(1)];
			for (int i = 0; i < spots.Length; i++)
			{
				spots[i] = new Point { Y = points[routes[r, i] - 1, 0] - 1, X = points[routes[r, i] - 1, 1] - 1 };
			}

			var route = GetRoute(spots);
			Robot robot = new Robot(route);
			robots.Add(robot);
		}

		Dictionary<Point, List<Robot>> checkCrashed = new Dictionary<Point, List<Robot>>();
		Stack<Robot> done = new Stack<Robot>();
		while (robots.Count > 0)
		{
			checkCrashed.Clear();
			done.Clear();

			foreach (Robot robot in robots)
			{
				robot.Act();
				if (robot.Done()) done.Push(robot);

				if (!checkCrashed.ContainsKey(robot.Current))
					checkCrashed.Add(robot.Current, new List<Robot>());
				checkCrashed[robot.Current].Add(robot);
			}

			foreach (List<Robot> r in checkCrashed.Values)
			{
				if (r.Count > 1)
				{
					answer++;
				}
			}

			foreach (var robot in done)
			{
				robots.Remove(robot);
			}
		}

		return answer;
	}
}