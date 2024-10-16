using System;

public class Duration
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    // Constructor to accept hours, minutes, seconds
    public Duration(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        NormalizeTime();
    }

    // Constructor to accept total seconds
    public Duration(int totalSeconds)
    {
        Hours = totalSeconds / 3600;
        Minutes = (totalSeconds % 3600) / 60;
        Seconds = totalSeconds % 60;
    }

    // Override ToString to format the output
    public override string ToString()
    {
        return $"{Hours}H:{Minutes}M:{Seconds}S";
    }

    // Override Equals for comparison
    public override bool Equals(object obj)
    {
        if (obj is Duration other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Hours, Minutes, Seconds).GetHashCode();
    }

    // Operator overload for addition
    public static Duration operator +(Duration d1, Duration d2)
    {
        return new Duration(d1.Hours + d2.Hours, d1.Minutes + d2.Minutes, d1.Seconds + d2.Seconds);
    }

    // Operator overload to add seconds to a Duration
    public static Duration operator +(int seconds, Duration d)
    {
        return new Duration(d.TotalSeconds() + seconds);
    }

    // Operator overload to increment by one minute (D1++)
    public static Duration operator ++(Duration d)
    {
        d.Minutes++;
        d.NormalizeTime();
        return d;
    }

    // Operator overload to decrement by one minute (--D1)
    public static Duration operator --(Duration d)
    {
        d.Minutes--;
        d.NormalizeTime();
        return d;
    }

    // Operator overload for comparison (<=)
    public static bool operator <=(Duration d1, Duration d2)
    {
        return d1.TotalSeconds() <= d2.TotalSeconds();
    }

    // Operator overload for comparison (>=)
    public static bool operator >=(Duration d1, Duration d2)
    {
        return d1.TotalSeconds() >= d2.TotalSeconds();
    }

    // Helper method to normalize time
    private void NormalizeTime()
    {
        if (Seconds >= 60)
        {
            Minutes += Seconds / 60;
            Seconds %= 60;
        }

        if (Minutes >= 60)
        {
            Hours += Minutes / 60;
            Minutes %= 60;
        }

        if (Seconds < 0)
        {
            Minutes--;
            Seconds += 60;
        }

        if (Minutes < 0)
        {
            Hours--;
            Minutes += 60;
        }
    }

    // Helper method to get the total seconds
    private int TotalSeconds()
    {
        return Hours * 3600 + Minutes * 60 + Seconds;
    }
}

// Testing the implementation
public class Program
{
    public static void Main(string[] args)
    {
        Duration D1 = new Duration(1, 10, 15);  // 1 hour, 10 minutes, 15 seconds
        Duration D2 = new Duration(7800);       // 7800 seconds = 2 hours, 10 minutes

        Console.WriteLine(D1.ToString());
        Console.WriteLine(D2.ToString());

        Duration D3 = D1 + D2;
        Console.WriteLine(D3.ToString());

        D3 = 666 + D3;
        Console.WriteLine(D3.ToString()); 

        D3++;
        Console.WriteLine(D3.ToString()); 

        --D3;
        Console.WriteLine(D3.ToString()); 

        Console.WriteLine(D1 <= D2);  
    }
}
