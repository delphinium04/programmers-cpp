using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] solution(int num, int total) {
        int[] answer = new int[] {};
        
        List<int> a;
        
        int mid = total/num;
        if(num%2==0){
            a = new List<int>(Enumerable.Range(mid - (num/2) + 1, num));
            answer = a.ToArray();
        }
        else{
            a = new List<int>(Enumerable.Range(mid-num/2, num));
            answer = a.ToArray();
        }
        
        return answer;
    }
}