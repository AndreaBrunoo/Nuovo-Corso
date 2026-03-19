
namespace Rubrica.Api.Helpers;

public static class TimeIntelligence
{
    public static int CalcolaEta(DateTime dataDiNascita)
    {
        DateTime oggi = DateTime.Today;

        int eta = oggi.Year - dataDiNascita.Year;

        // Se il compleanno non è ancora passato quest'anno, sottrai 1
        if (oggi < dataDiNascita.AddYears(eta))
        {
            eta--;
        }

        return eta;
    }
}