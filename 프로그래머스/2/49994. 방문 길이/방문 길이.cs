using System;
using System.Collections.Generic;

public class Solution {    
    public struct Point{
        public int x;
        public int y;
    }
    Dictionary<Point, HashSet<Point>> connect = new Dictionary<Point, HashSet<Point>>();
    
    public void Add(Point from, Point to){
        if(!connect.ContainsKey(from)){
            HashSet<Point> s = new HashSet<Point>();
            connect.Add(from, s);
        }
        if(!connect.ContainsKey(to)){
            HashSet<Point> s = new HashSet<Point>();
            connect.Add(to, s);
        }
        
        connect[from].Add(to);
        connect[to].Add(from);
    }
    
    public int GetLine()
    {
        int c = 0;
        foreach(HashSet<Point> h in connect.Values){
            c += h.Count;
        }
        return c/2;
    }
    
    public int solution(string dirs) {       
        Point p;
        p.x=0;
        p.y=0;
        for(int i=0;i<dirs.Length;i++){
            char dir = dirs[i];
            Point t;
            t.x = p.x;
            t.y = p.y;
            
            if(dir == 'U') t.y++;
            if(dir == 'D') t.y--;
            if(dir == 'L') t.x--;
            if(dir == 'R') t.x++;
            
            if(!(-5 <= t.x && t.x <= 5) ||
               !(-5 <= t.y && t.y <= 5)) continue;
                        
            if(t.x != p.x) Add(p, t);
            else Add(p, t);
            p = t;
        }
        
        return GetLine();
    }
}