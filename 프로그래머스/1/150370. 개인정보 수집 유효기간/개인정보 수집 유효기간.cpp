#include <string>
#include <vector>
#include <sstream>
#include <map>

using namespace std;

struct Date
{
public:
    short year;
    short month;
    short day;

    void addMonth(short m)
    {
        month += m;
        if (month >= 13)
        {
            year += (month - 1) / 12;
            month = (month - 1) % 12 + 1;
        }
    }

    void parse(string s)
    {
        year = stoi(s.substr(0, 4));
        month = stoi(s.substr(5, 2));
        day = stoi(s.substr(8, 2));
    }

    short Compare(Date *d)
    {
        if (year > d->year)
            return 1;
        if (year < d->year)
            return -1;

        if (month > d->month)
            return 1;
        if (month < d->month)
            return -1;

        if (day > d->day)
            return 1;
        if (day < d->day)
            return -1;

        return 0;
    }

    string print()
    {
        ostringstream ss;
        ss << year << "." << month << "." << day;
        return ss.str();
    }

} typedef Date;

vector<int> solution(string today, vector<string> terms, vector<string> privacies)
{
    vector<int> answer;

    Date todayDate;
    todayDate.parse(today);

    map<char, int> termsMap;
    for (int i = 0; i < terms.size(); i++)
    {
        istringstream ss(terms[i]);
        char key;
        int value;
        ss >> key >> value;
        termsMap.emplace(key, value);
    }

    for (int i = 0; i < privacies.size(); i++)
    {
        istringstream pCase(privacies[i]);
        Date cmpDate, stdDate;
        string date, privacyType;
        pCase >> date >> privacyType;

        cmpDate.parse(date);
        cmpDate.addMonth(termsMap[privacyType[0]]);

        if (cmpDate.Compare(&todayDate) <= 0)
            answer.push_back(i + 1);
    }

    return answer;
}