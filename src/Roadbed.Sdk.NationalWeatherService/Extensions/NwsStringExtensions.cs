/*
 * The namespace Roadbed.Sdk.NationalWeatherService.Extensions was removed on purpose and replaced with Roadbed.Sdk.NationalWeatherService so that no additional using statements are required.
 */

namespace Roadbed.Sdk.NationalWeatherService;

/// <summary>
/// Extensions for weather String operations.
/// </summary>
public static class NwsStringExtensions
{
    private static readonly string[] ValidWFOList = new string[]
        {
            "AKQ", "ALY", "BGM", "BOX", "BTV", "BUF", "CAE", "CAR", "CHS", "CLE", "CTP", "GSP", "GYX", "ILM", "ILN", "LWX", "MHX", "OKX", "PBZ", "PHI", "RAH",
            "RLX", "RNK", "ABQ", "AMA", "BMX", "BRO", "CRP", "EPZ", "EWX", "FFC", "FWD", "HGX", "HUN", "JAN", "JAX", "KEY", "LCH", "LIX", "LUB", "LZK", "MAF",
            "MEG", "MFL", "MLB", "MOB", "MRX", "OHX", "OUN", "SHV", "SJT", "SJU", "TAE", "TBW", "TSA", "ABR", "APX", "ARX", "BIS", "BOU", "CYS", "DDC", "DLH",
            "DMX", "DTX", "DVN", "EAX", "FGF", "FSD", "GID", "GJT", "GLD", "GRB", "GRR", "ICT", "ILX", "IND", "IWX", "JKL", "LBF", "LMK", "LOT", "LSX", "MKX",
            "MPX", "MQT", "OAX", "PAH", "PUB", "RIW", "SGF", "TOP", "UNR", "BOI", "BYZ", "EKA", "FGZ", "GGW", "HNX", "LKN", "LOX", "MFR", "MSO", "MTR", "OTX",
            "PDT", "PIH", "PQR", "PSR", "REV", "SEW", "SGX", "SLC", "STO", "TFX", "TWC", "VEF", "AER", "AFC", "AFG", "AJK", "ALU", "GUM", "HPA", "HFO", "PPG",
            "STU", "NH1", "NH2", "ONA", "ONP",
        };

    private static readonly string[] ValidStateList = new string[]
    {
            "AL", "AK", "AS", "AR", "AZ", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI",
            "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI",
            "VA", "WA", "WV", "WI", "WY",
    };

    /// <summary>
    /// Verifies a string value is a a valid Weather Forecast Office (WFO) identifier.
    /// </summary>
    /// <param name="str">String value to check.</param>
    /// <returns>Indication on whether the string value was a valid Weather Forecast Office (WFO) identifier.</returns>
    public static bool IsValidWFO(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        return ValidWFOList.Contains(str.ToUpper());
    }

    /// <summary>
    /// Verifies a string value is a a valid 2-Character State identifier.
    /// </summary>
    /// <param name="str">String value to check.</param>
    /// <returns>Indication on whether the string value was a valid State identifier.</returns>
    public static bool IsValidState(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        return ValidStateList.Contains(str.ToUpper());
    }
}
