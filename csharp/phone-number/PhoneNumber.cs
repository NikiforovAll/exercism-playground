using System;
using System.Linq;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        var numbers = phoneNumber.Where(c => char.IsDigit(c));
        var n = numbers.Count();
        switch (n)
        {
            case 11:
                var countryCode = numbers.First();
                if (countryCode != '1')
                    throw new ArgumentException(nameof(phoneNumber));
                numbers = numbers.Skip(1);
                break;
            case 10:
                break;
            default:
                throw new ArgumentException(nameof(phoneNumber));
        }

        var indexed = numbers.ToArray();
        // validate (NXX)-NXX-XXXX
        if (!new char[] { indexed[0], indexed[3] }
            .Select(c => char.GetNumericValue(c)).All(d => d > 1))
        {
            throw new ArgumentException(nameof(phoneNumber));
        };

        return new string(indexed);

    }
}
