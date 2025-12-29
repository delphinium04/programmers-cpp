#include <string>
#include <vector>

using namespace std;

int solution(vector<int> players, int m, int k)
{
    int answer = 0;

    vector<int> schedule(24);
    int currentServer = 1;

    for (int i = 0; i < players.size(); i++)
    {
        // 반납
        currentServer -= schedule[i];

        // 가용량 확인 및 대여
        if (players[i] >= currentServer * m)
        {
            int neededServerCount = players[i] / m - currentServer + 1;
            currentServer += neededServerCount;
            if (i + k < schedule.size())
            {
                schedule[i + k] = neededServerCount;
            }

            answer += neededServerCount;
        }
    }

    return answer;
}